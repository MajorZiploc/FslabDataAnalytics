module FsLabDataAnalytics.osrs

open System
open System.IO
open System.Collections.Generic
open FSharp.Stats
open FSharp.Data
open Deedle
open Newtonsoft.Json

type Config = {
    fileLocations: Dictionary<string,string>
}

let run argv =
    let thisDir = utils.getCurrentDir "osrs"
    let configText = File.ReadAllText(Path.Combine(thisDir, "config.json"))
    let config = JsonConvert.DeserializeObject<Config>(configText)
    utils.setFullPaths (config.fileLocations) thisDir
    printfn "%A" <| config.fileLocations.["data"]
    let df = Frame.ReadCsv(config.fileLocations.["data"],hasHeaders=true,separators=",")
    ["web-scraper-order"; "web-scraper-start-url"; "char_link-href"]
    |> List.iter (fun colName ->
        df.DropColumn(colName)
    )
    // df.Print()

    printfn "The new frame does now contain: %i rows and %i columns" df.RowCount df.ColumnCount
    // Prints column names
    printfn "%A" <| df.Columns.Keys

    // let housesNotAtRiver = 
    //     df
    //     |> Frame.sliceCols ["RoomsPerDwelling";"MedianHomeValue";"CharlesRiver"]
    //     |> Frame.filterRowValues (fun s -> s.GetAs<bool>("CharlesRiver") |> not ) 

    //sprintf "The new frame does now contain: %i rows and %i columns" housesNotAtRiver.RowCount housesNotAtRiver.ColumnCount

    // housesNotAtRiver.Print()

    // let x =
    //     housesNotAtRiver
    //     |> Frame.melt
    // x.Print()

    // [6.0; factorialOf3]
    // |> Seq.iter (printfn "%A")
    0 // return an integer exit code