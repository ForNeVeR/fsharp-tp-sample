module Sample.TypeProvider

open ProviderImplementation
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection

[<TypeProvider>]
type BasicProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "StaticProperty.Provided"
    let asm = Assembly.GetExecutingAssembly()
    let ctxt = ProvidedTypesContext.Create(config, isForGenerated=true)

    let createTypes () =
        let myType = ctxt.ProvidedTypeDefinition(asm, ns, "MyType", Some typeof<obj>, isErased = false)
        let myProp = ctxt.ProvidedProperty("MyProperty", typeof<string>, isStatic = true, getterCode = (fun args -> <@@ "Hello world" @@>))
        myType.AddMember(myProp)
        [myType]

    do
        this.AddNamespace(ns, createTypes())

[<assembly:TypeProviderAssembly>]
do ()