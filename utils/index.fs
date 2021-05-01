module fslab_data_analytics.utils

open System.IO
open System.Collections.Generic

let setFullPaths (fileLocations: Dictionary<string,string>) (dir: string) =
  for entry in fileLocations do
    fileLocations.[entry.Key] <- Path.Combine(dir, entry.Value)

let getCurrentDir (fileName: string) =
  let thisDir = Path.Combine(Directory.GetCurrentDirectory(), "fslab_data_analytics", fileName)
  thisDir
