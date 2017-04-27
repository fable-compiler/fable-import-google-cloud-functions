module Util

open Fable.Core
open Fable.Import

let Assert = Node.``assert``

type Mocha =
  [<Global>]
  abstract describe: string * (unit -> unit) -> unit
  [<Global>]
  abstract it: string * (unit -> unit) -> unit
  [<Global>]
  abstract it: string * ((unit -> unit) -> unit) -> unit
  [<Global>]
  abstract it: string * (unit -> JS.Promise<'a>) -> unit

[<Import("*","assert")>]
let m : Mocha = jsNative

let inline describe name tests = m.describe(name, tests)
let inline it name (tests : unit -> unit) = m.it(name, tests)
let inline itWithCallback name (tests : (unit -> unit) -> unit) = m.it(name, tests)
let inline itPromises name (tests : unit -> JS.Promise<_>) = m.it(name, tests)
