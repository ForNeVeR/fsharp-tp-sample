module Sample.TypeProvider

open ProviderImplementation
open ProviderImplementation.ProvidedTypes
open Microsoft.FSharp.Core.CompilerServices
open System.Reflection
open System.IO

[<TypeProvider>]
type BasicProvider (config : TypeProviderConfig) as this =
    inherit TypeProviderForNamespaces ()

    let ns = "StaticProperty.Provided"
    let asm = Assembly.LoadFrom(config.RuntimeAssembly)
    let ctxt = ProvidedTypesContext.Create(config, isForGenerated=true)

    let createTypes () =
        let myType = ctxt.ProvidedTypeDefinition(asm, ns, "MyType", Some typeof<obj>, isErased = false)
        let myProp = ctxt.ProvidedProperty("MyProperty", typeof<string>, isStatic = true, getterCode = (fun args -> <@@ "Hello world" @@>))
        myType.AddMember(myProp)
        [myType]

    do
        let types = createTypes()
        let assemblyName = Path.ChangeExtension(Path.GetTempFileName(), ".dll")
        let pa = ProvidedAssembly(assemblyName)
        pa.AddTypes types
        this.AddNamespace(ns, types)

[<assembly:TypeProviderAssembly>]
do ()