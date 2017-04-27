module Fable.Import.Google.Cloud.Functions

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import

type [<Erase>] RawEventData = RawEventData of string
type [<Erase>] EventId = EventId of string

type GoogleCloudFunctionsEventData =
  { data: RawEventData
    attributes: Map<string,string> }

type GoogleCloudFunctionsEvent =
  { eventId: EventId
    timestamp: System.DateTime
    eventType: string
    resource: string
    data: GoogleCloudFunctionsEventData }

module RawEventData =
  /// Temporary internal mapping for working with Node buffers
  type private MyBuffer =
    inherit Node.Buffer
    [<Emit("new $0 ($1...)")>]
    abstract Create: str:string * ?encoding:string -> Node.Buffer

  [<Global>]
  let private Buffer = (Node.Buffer :?> MyBuffer)

  let packString str =
      Buffer.Create(str).toString("base64")
      |> RawEventData

  let packJSON data =
      JS.JSON.stringify data |> packString

  let extractString (RawEventData data) =
      Buffer.from(data, "base64").toString()

  let extractJSON data =
      extractString data |> JS.JSON.parse |> unbox
