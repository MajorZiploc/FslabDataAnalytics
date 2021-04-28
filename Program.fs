// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open FSharp.Stats
open FSharp.Data
open Deedle

[<EntryPoint>]
let main argv =
    let factorialOf3 = SpecialFunctions.Factorial.factorial 3
    // Retrieve data using the FSharp.Data package
    let rawData = Http.RequestString @"https://raw.githubusercontent.com/dotnet/machinelearning/master/test/data/housing.txt"

    // Use .net Core functions to convert the retrieved string to a stream
    let dataAsStream = new System.IO.MemoryStream(rawData |> System.Text.Encoding.UTF8.GetBytes) 

    // And finally create a data frame object using the ReadCsv method provided by Deedle.
    // Note: Of course you can directly provide the path to a local source.
    let dataAsFrame = Frame.ReadCsv(dataAsStream,hasHeaders=true,separators="\t")

    // Using the Print() method, we can use the Deedle pretty printer to have a look at the data set.
    dataAsFrame.Print()

    let housesNotAtRiver = 
        dataAsFrame
        |> Frame.sliceCols ["RoomsPerDwelling";"MedianHomeValue";"CharlesRiver"]
        |> Frame.filterRowValues (fun s -> s.GetAs<bool>("CharlesRiver") |> not ) 

    //sprintf "The new frame does now contain: %i rows and %i columns" housesNotAtRiver.RowCount housesNotAtRiver.ColumnCount

    housesNotAtRiver.Print()

    let x =
        housesNotAtRiver
        |> Frame.melt
    x.Print()

    [6.0; factorialOf3]
    |> Seq.iter (printfn "%A")
    0 // return an integer exit code