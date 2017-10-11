#r "./bin/Debug/net47/fsharp-tp-sample.dll"
open StaticProperty.Provided
type MT = MyType
printfn "%s" MT.MyProperty
printfn "%A" typeof<MT>