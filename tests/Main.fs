module Tests

open Fable.Core
open Fable.Import
open Util
open Fable.Import.Google.Cloud.Functions

type MyRecord =
  { myStr: string
    myInt: int }

let tests () =
  describe "Google Functions" <| fun _ ->
    describe "RawEventMetadata" <| fun _ ->
      describe "packString" <| fun _ ->
        it "is ok" <| fun _ ->
          Assert.ok(RawEventData.packString "testStr")
      describe "packJSON" <| fun _ ->
        it "is ok" <| fun _ ->
          Assert.ok(RawEventData.packJSON { myStr = "testStr"; myInt = 42 })
      it "can roundtrip a string" <| fun () ->
        let initial = "testStr"
        let final =
          RawEventData.packString initial
          |> RawEventData.extractString
        Assert.equal(final, initial)
      it "can roundtrip some JSON" <| fun () ->
        let initial = { myStr = "testStr"; myInt = 42 }
        let final =
          RawEventData.packJSON initial
          |> RawEventData.extractJSON
        Assert.deepEqual(final, initial)

tests ()
