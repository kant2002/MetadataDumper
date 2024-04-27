using dnlib.DotNet;
using dnlib.DotNet.Emit;

if (args.Length < 1)
{
    Console.WriteLine("MetadataDumper mdfile [targetdirectory]");
    return;
}

var assemblyFile = args[0];
var targetDirectory = args.ElementAtOrDefault(1) ?? Directory.GetCurrentDirectory();

ModuleContext modCtx = ModuleDef.CreateModuleContext();
ModuleDefMD module = ModuleDefMD.Load(assemblyFile, modCtx);
var metadata = module.Metadata;
DumpMetadata(targetDirectory);

void DumpMetadata(string folder)
{
    PrintModuleTable(new StreamWriter(File.Open(Path.Combine(folder, Path.Combine(folder, "Module.csv")), FileMode.Create)));
    PrintTypeRefTable(new StreamWriter(File.Open(Path.Combine(folder, "TypeRef.csv"), FileMode.Create)));
    PrintTypeDefTable(new StreamWriter(File.Open(Path.Combine(folder, "TypeDef.csv"), FileMode.Create)));
    PrintFieldPtrTable(new StreamWriter(File.Open(Path.Combine(folder, "FieldPtr.csv"), FileMode.Create)));
    PrintFieldTable(new StreamWriter(File.Open(Path.Combine(folder, "Field.csv"), FileMode.Create)));
    PrintMethodPtrTable(new StreamWriter(File.Open(Path.Combine(folder, "MethodPtr.csv"), FileMode.Create)));
    PrintMethodTable(new StreamWriter(File.Open(Path.Combine(folder, "Method.csv"), FileMode.Create)));
    PrintParamPtrTable(new StreamWriter(File.Open(Path.Combine(folder, "ParamPtr.csv"), FileMode.Create)));
    PrintParamTable(new StreamWriter(File.Open(Path.Combine(folder, "Param.csv"), FileMode.Create)));
    PrintInterfaceImplTable(new StreamWriter(File.Open(Path.Combine(folder, "InterfaceImpl.csv"), FileMode.Create)));
    PrintMemberRefTable(new StreamWriter(File.Open(Path.Combine(folder, "MemberRef.csv"), FileMode.Create)));
    PrintConstantTable(new StreamWriter(File.Open(Path.Combine(folder, "Constant.csv"), FileMode.Create)));
    PrintCustomAttributeTable(new StreamWriter(File.Open(Path.Combine(folder, "CustomAttribute.csv"), FileMode.Create)));
    PrintFieldMarshalTable(new StreamWriter(File.Open(Path.Combine(folder, "FieldMarshal.csv"), FileMode.Create)));
    PrintDeclSecurityTable(new StreamWriter(File.Open(Path.Combine(folder, "DeclSecurity.csv"), FileMode.Create)));
    PrintClassLayoutTable(new StreamWriter(File.Open(Path.Combine(folder, "ClassLayout.csv"), FileMode.Create)));
    PrintFieldLayoutTable(new StreamWriter(File.Open(Path.Combine(folder, "FieldLayout.csv"), FileMode.Create)));
    PrintStandAloneSigTable(new StreamWriter(File.Open(Path.Combine(folder, "StandAloneSig.csv"), FileMode.Create))); // Should decode StandAlone Signatures
    PrintEventMapTable(new StreamWriter(File.Open(Path.Combine(folder, "EventMap.csv"), FileMode.Create)));
    PrintEventPtrTable(new StreamWriter(File.Open(Path.Combine(folder, "EventPtr.csv"), FileMode.Create)));
    PrintEventTable(new StreamWriter(File.Open(Path.Combine(folder, "Event.csv"), FileMode.Create)));
    PrintPropertyMapTable(new StreamWriter(File.Open(Path.Combine(folder, "PropertyMap.csv"), FileMode.Create)));
    PrintPropertyPtrTable(new StreamWriter(File.Open(Path.Combine(folder, "PropertyPtr.csv"), FileMode.Create)));
    PrintPropertyTable(new StreamWriter(File.Open(Path.Combine(folder, "Property.csv"), FileMode.Create)));
    //PrintMethodSemanticsTable(new StreamWriter(File.Open(Path.Combine(folder, "MethodSemantics.csv"), FileMode.Create)));
    //PrintMethodImplTable(new StreamWriter(File.Open(Path.Combine(folder, "MethodImpl.csv"), FileMode.Create)));
    PrintModuleRefTable(new StreamWriter(File.Open(Path.Combine(folder, "ModuleRef.csv"), FileMode.Create)));
    //PrintTypeSpecTable(new StreamWriter(File.Open(Path.Combine(folder, "TypeSpec.csv"), FileMode.Create)));
    //PrintImplMapTable(new StreamWriter(File.Open(Path.Combine(folder, "ImplMap.csv"), FileMode.Create)));
    //PrintFieldRVATable(new StreamWriter(File.Open(Path.Combine(folder, "FieldRVA.csv"), FileMode.Create)));
    //PrintENCLogTable(new StreamWriter(File.Open(Path.Combine(folder, "ENCLog.csv"), FileMode.Create)));
    //PrintENCMapTable(new StreamWriter(File.Open(Path.Combine(folder, "ENCMap.csv"), FileMode.Create)));
    PrintAssemblyTable(new StreamWriter(File.Open(Path.Combine(folder, "Assembly.csv"), FileMode.Create)));
    //PrintAssemblyProcessorTable(new StreamWriter(File.Open(Path.Combine(folder, "AssemblyProcessor.csv"), FileMode.Create)));
    //PrintAssemblyOSTable(new StreamWriter(File.Open(Path.Combine(folder, "AssemblyOS.csv"), FileMode.Create)));
    PrintAssemblyRefTable(new StreamWriter(File.Open(Path.Combine(folder, "AssemblyRef.csv"), FileMode.Create)));
    //PrintAssemblyRefProcessorTable(new StreamWriter(File.Open(Path.Combine(folder, "AssemblyRefProcessor.csv"), FileMode.Create)));
    //PrintAssemblyRefOSTable(new StreamWriter(File.Open(Path.Combine(folder, "AssemblyRefOS.csv"), FileMode.Create)));
    //PrintFileTable(new StreamWriter(File.Open(Path.Combine(folder, "File.csv"), FileMode.Create)));
    PrintExportedTypeTable(new StreamWriter(File.Open(Path.Combine(folder, "ExportedType.csv"), FileMode.Create)));
    PrintManifestResourceTable(new StreamWriter(File.Open(Path.Combine(folder, "ManifestResource.csv"), FileMode.Create)));
    PrintNestedClassTable(new StreamWriter(File.Open(Path.Combine(folder, "NestedClass.csv"), FileMode.Create)));
    PrintGenericParamTable(new StreamWriter(File.Open(Path.Combine(folder, "GenericParam.csv"), FileMode.Create)));
    //PrintMethodSpecTable(new StreamWriter(File.Open(Path.Combine(folder, "MethodSpec.csv"), FileMode.Create)));
    //PrintGenericParamConstraintTable(new StreamWriter(File.Open(Path.Combine(folder, "GenericParamConstraint.csv"), FileMode.Create)));

    //PrintDocumentTable(new StreamWriter(File.Open(Path.Combine(folder, "Document.csv"), FileMode.Create)));
    //PrintMethodDebugInformationTable(new StreamWriter(File.Open(Path.Combine(folder, "MethodDebugInformation.csv"), FileMode.Create)));
    //PrintLocalScopeTable(new StreamWriter(File.Open(Path.Combine(folder, "LocalScope.csv"), FileMode.Create)));
    //PrintLocalVariableTable(new StreamWriter(File.Open(Path.Combine(folder, "LocalVariable.csv"), FileMode.Create)));
    //PrintLocalConstantTable(new StreamWriter(File.Open(Path.Combine(folder, "LocalConstant.csv"), FileMode.Create)));
    //PrintImportScopeTable(new StreamWriter(File.Open(Path.Combine(folder, "ImportScope.csv"), FileMode.Create)));
    //PrintStateMachineMethodTable(new StreamWriter(File.Open(Path.Combine(folder, "StateMachineMethod.csv"), FileMode.Create)));
    //PrintCustomDebugInformationTable(new StreamWriter(File.Open(Path.Combine(folder, "CustomDebugInformation.csv"), FileMode.Create)));
}

void PrintAssemblyTable(TextWriter writer)
{
    writer.WriteLine("RID,HashAlgId,MajorVersion,MinorVersion,BuildNumber,RevisionNumber,Flags,PublicKey,Name,Locale");
    for (uint i = 1; i <= metadata.TablesStream.AssemblyTable.Rows; i++)
    {
        metadata.TablesStream.TryReadAssemblyRow(i, out var assemblyRow);
        writer.WriteLine($"{i},{(AssemblyHashAlgorithm)assemblyRow.HashAlgId},{assemblyRow.MajorVersion},{assemblyRow.MinorVersion},{assemblyRow.BuildNumber},{assemblyRow.RevisionNumber},{assemblyRow.Flags},{GetBytes(assemblyRow.PublicKey)},{GetName(assemblyRow.Name)},{GetName(assemblyRow.Locale)}");
    }

    writer.Flush();
}

void PrintAssemblyRefTable(TextWriter writer)
{
    writer.WriteLine("RID,MajorVersion,MinorVersion,BuildNumber,RevisionNumber,Flags,PublicKeyOrToken,Name,Locale");
    for (var i = 1; i <= metadata.TablesStream.AssemblyRefTable.Rows; i++)
    {
        metadata.TablesStream.TryReadAssemblyRefRow(1, out var assemblyRow);
        writer.WriteLine($"{i},{assemblyRow.MajorVersion},{assemblyRow.MinorVersion},{assemblyRow.BuildNumber},{assemblyRow.RevisionNumber},{assemblyRow.Flags},{GetPublicKeyOrToken(assemblyRow.Flags, assemblyRow.PublicKeyOrToken)},{GetName(assemblyRow.Name)},{GetName(assemblyRow.Locale)}");
    }

    writer.Flush();
}

void PrintModuleTable(TextWriter writer)
{
    writer.WriteLine("RID,Generation,Name,Mvid,EncId,EncBaseId");
    for (uint i = 1; i <= metadata.TablesStream.ModuleTable.Rows; i++)
    {
        metadata.TablesStream.TryReadModuleRow(i, out var row);
        writer.WriteLine($"{i},{row.Generation},{GetName(row.Name)},{row.Mvid},{row.EncId},{row.EncBaseId}");
    }

    writer.Flush();
}

void PrintTypeRefTable(TextWriter writer)
{
    writer.WriteLine("RID,ResolutionScope,Name,Namespace");
    for (uint i = 1; i <= metadata.TablesStream.TypeRefTable.Rows; i++)
    {
        metadata.TablesStream.TryReadTypeRefRow(i, out var row);
        writer.WriteLine($"{i},{row.ResolutionScope},{GetName(row.Name)},{GetName(row.Namespace)}");
    }

    writer.Flush();
}

void PrintTypeDefTable(TextWriter writer)
{
    writer.WriteLine("RID,Flags,Name,Namespace,Extends,FieldList,MethodList");
    for (uint i = 1; i <= metadata.TablesStream.TypeDefTable.Rows; i++)
    {
        metadata.TablesStream.TryReadTypeDefRow(i, out var row);
        writer.WriteLine($"{i},{row.Flags},{GetName(row.Name)},{GetName(row.Namespace)},{row.Extends},{row.FieldList},{row.MethodList}");
    }

    writer.Flush();
}

void PrintFieldPtrTable(TextWriter writer)
{
    var table = metadata.TablesStream.FieldPtrTable;
    writer.WriteLine("RID,Field");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadFieldPtrRow(i, out var row);
        writer.WriteLine($"{i},{GetName(row.Field)}");
    }

    writer.Flush();
}

void PrintFieldTable(TextWriter writer)
{
    var table = metadata.TablesStream.FieldTable;
    writer.WriteLine("RID,Flags,Name,Signature");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadFieldRow(i, out var row);
        writer.WriteLine($"{i},{row.Flags},{GetName(row.Name)},{row.Signature}");
    }

    writer.Flush();
}

void PrintMethodPtrTable(TextWriter writer)
{
    var table = metadata.TablesStream.MethodPtrTable;
    writer.WriteLine("RID,Method");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadMethodPtrRow(i, out var row);
        writer.WriteLine($"{i},{GetName(row.Method)}");
    }

    writer.Flush();
}

void PrintMethodTable(TextWriter writer)
{
    var table = metadata.TablesStream.MethodTable;
    writer.WriteLine("RID,RVA,ImplFlags,Flags,Name,Signature,ParamList");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadMethodRow(i, out var row);
        writer.WriteLine($"{i},{row.RVA},{row.ImplFlags},{row.Flags},{GetName(row.Name)},{row.Signature},{row.ParamList}");
    }

    writer.Flush();
}

void PrintParamPtrTable(TextWriter writer)
{
    var table = metadata.TablesStream.ParamPtrTable;
    writer.WriteLine("RID,Param");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadParamPtrRow(i, out var row);
        writer.WriteLine($"{i},{GetName(row.Param)}");
    }

    writer.Flush();
}

void PrintParamTable(TextWriter writer)
{
    var table = metadata.TablesStream.ParamTable;
    writer.WriteLine("RID,Flags,Sequence,Name");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadParamRow(i, out var row);
        writer.WriteLine($"{i},{row.Flags},{row.Sequence},{GetName(row.Name)}");
    }

    writer.Flush();
}

void PrintInterfaceImplTable(TextWriter writer)
{
    var table = metadata.TablesStream.InterfaceImplTable;
    writer.WriteLine("RID,Class,Interface");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadInterfaceImplRow(i, out var row);
        writer.WriteLine($"{i},{row.Class},{row.Interface}");
    }

    writer.Flush();
}

void PrintMemberRefTable(TextWriter writer)
{
    var table = metadata.TablesStream.MemberRefTable;
    writer.WriteLine("RID,Class,Name,Signature");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadMemberRefRow(i, out var row);
        writer.WriteLine($"{i},{row.Class},{GetName(row.Name)},{row.Signature}");
    }

    writer.Flush();
}

void PrintConstantTable(TextWriter writer)
{
    var table = metadata.TablesStream.ConstantTable;
    writer.WriteLine("RID,Type,Padding,Parent,Value");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadConstantRow(i, out var row);
        var elementType = (ElementType)row.Type;
        writer.WriteLine($"{i},{elementType},{row.Padding},{row.Parent},{GetValue(elementType, row.Value)}");
    }

    writer.Flush();
}

void PrintCustomAttributeTable(TextWriter writer)
{
    var table = metadata.TablesStream.CustomAttributeTable;
    writer.WriteLine("RID,Parent,Type,Value");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadCustomAttributeRow(i, out var row);
        writer.WriteLine($"{i},{row.Parent},{row.Type},{row.Value}");
    }

    writer.Flush();
}

void PrintFieldMarshalTable(TextWriter writer)
{
    var table = metadata.TablesStream.FieldMarshalTable;
    writer.WriteLine("RID,Parent,NativeType");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadFieldMarshalRow(i, out var row);
        writer.WriteLine($"{i},{row.Parent},{GetBytes(row.NativeType)}");
    }

    writer.Flush();
}

void PrintDeclSecurityTable(TextWriter writer)
{
    var table = metadata.TablesStream.DeclSecurityTable;
    writer.WriteLine("RID,Action,Parent,PermissionSet");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadDeclSecurityRow(i, out var row);
        writer.WriteLine($"{i},{(SecurityAction)row.Action},{row.Parent},{GetBytes(row.PermissionSet)}");
    }

    writer.Flush();
}

void PrintClassLayoutTable(TextWriter writer)
{
    var table = metadata.TablesStream.ClassLayoutTable;
    writer.WriteLine("RID,PackingSize,ClassSize,Parent");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadClassLayoutRow(i, out var row);
        writer.WriteLine($"{i},{row.PackingSize},{row.ClassSize},{row.Parent}");
    }

    writer.Flush();
}

void PrintFieldLayoutTable(TextWriter writer)
{
    var table = metadata.TablesStream.FieldLayoutTable;
    writer.WriteLine("RID,OffSet,Field");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadFieldLayoutRow(i, out var row);
        writer.WriteLine($"{i},{row.OffSet},{row.Field}");
    }

    writer.Flush();
}

void PrintStandAloneSigTable(TextWriter writer)
{
    var table = metadata.TablesStream.StandAloneSigTable;
    writer.WriteLine("RID,Signature");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadStandAloneSigRow(i, out var row);
        writer.WriteLine($"{i},{row.Signature}");
    }

    writer.Flush();
}

void PrintEventMapTable(TextWriter writer)
{
    var table = metadata.TablesStream.EventMapTable;
    writer.WriteLine("RID,Parent,EventList");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadEventMapRow(i, out var row);
        writer.WriteLine($"{i},{row.Parent},{row.EventList}");
    }

    writer.Flush();
}

void PrintEventPtrTable(TextWriter writer)
{
    var table = metadata.TablesStream.EventPtrTable;
    writer.WriteLine("RID,Event");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadEventPtrRow(i, out var row);
        writer.WriteLine($"{i},{GetName(row.Event)}");
    }

    writer.Flush();
}

void PrintEventTable(TextWriter writer)
{
    var table = metadata.TablesStream.EventTable;
    writer.WriteLine("RID,EventFlags,Name,EventType");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadEventRow(i, out var row);
        writer.WriteLine($"{i},{row.EventFlags},{GetName(row.Name)},{row.EventType}");
    }

    writer.Flush();
}

void PrintPropertyMapTable(TextWriter writer)
{
    var table = metadata.TablesStream.PropertyMapTable;
    writer.WriteLine("RID,Parent,PropertyList");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadPropertyMapRow(i, out var row);
        writer.WriteLine($"{i},{row.Parent},{row.PropertyList}");
    }

    writer.Flush();
}

void PrintPropertyPtrTable(TextWriter writer)
{
    var table = metadata.TablesStream.PropertyPtrTable;
    writer.WriteLine("RID,Property");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadPropertyPtrRow(i, out var row);
        writer.WriteLine($"{i},{GetName(row.Property)}");
    }

    writer.Flush();
}

void PrintPropertyTable(TextWriter writer)
{
    var table = metadata.TablesStream.PropertyTable;
    writer.WriteLine("RID,PropFlags,Name,Type");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadPropertyRow(i, out var row);
        writer.WriteLine($"{i},{row.PropFlags},{GetName(row.Name)},{row.Type}");
    }

    writer.Flush();
}

void PrintModuleRefTable(TextWriter writer)
{
    var table = metadata.TablesStream.ModuleRefTable;
    writer.WriteLine("RID,Name");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadModuleRefRow(i, out var row);
        writer.WriteLine($"{i},{GetName(row.Name)}");
    }

    writer.Flush();
}

void PrintExportedTypeTable(TextWriter writer)
{
    var table = metadata.TablesStream.ExportedTypeTable;
    writer.WriteLine("RID,Flags,TypeDefId,TypeName,TypeNamespace,Implementation");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadExportedTypeRow(i, out var row);
        writer.WriteLine($"{i},{row.Flags},{row.TypeDefId},{GetName(row.TypeName)},{GetName(row.TypeNamespace)},{row.Implementation}");
    }

    writer.Flush();
}

void PrintManifestResourceTable(TextWriter writer)
{
    var table = metadata.TablesStream.ManifestResourceTable;
    writer.WriteLine("RID,Offset,Flags,Name,Implementation");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadManifestResourceRow(i, out var row);
        writer.WriteLine($"{i},{row.Offset},{row.Flags},{GetName(row.Name)},{row.Implementation}");
    }

    writer.Flush();
}

void PrintNestedClassTable(TextWriter writer)
{
    var table = metadata.TablesStream.NestedClassTable;
    writer.WriteLine("RID,NestedClass,EnclosingClass");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadNestedClassRow(i, out var row);
        writer.WriteLine($"{i},{row.NestedClass},{row.EnclosingClass}");
    }

    writer.Flush();
}

void PrintGenericParamTable(TextWriter writer)
{
    var table = metadata.TablesStream.GenericParamTable;
    writer.WriteLine("RID,Number,Flags,Owner,Name,Kind");
    for (uint i = 1; i <= table.Rows; i++)
    {
        metadata.TablesStream.TryReadGenericParamRow(i, out var row);
        writer.WriteLine($"{i},{row.Number},{row.Flags},{row.Owner},{GetName(row.Name)},{row.Kind}");
    }

    writer.Flush();
}

string GetName(uint code)
{
    return metadata.StringsStream.ReadNoNull(code);
}
string GetBytes(uint code)
{
    return string.Join("", metadata.BlobStream.Read(code).Select(_ => _.ToString("X2")));
}

string GetPublicKeyOrToken(uint flags, uint code)
{
    return string.Join("", metadata.BlobStream.Read(code).Select(_ => _.ToString("X2")));
}
string? GetValue(ElementType type, uint code)
{
    var reader = metadata.BlobStream.CreateReader(code);
    return type switch
    {
        ElementType.Boolean => reader.ReadBoolean().ToString(),
        ElementType.Char => reader.ReadChar().ToString(),
        ElementType.I1 => reader.ReadSByte().ToString(),
        ElementType.U1 => reader.ReadByte().ToString(),
        ElementType.I2 => reader.ReadInt16().ToString(),
        ElementType.U2 => reader.ReadUInt16().ToString(),
        ElementType.I4 => reader.ReadInt32().ToString(),
        ElementType.U4 => reader.ReadUInt32().ToString(),
        ElementType.I8 => reader.ReadInt64().ToString(),
        ElementType.U8 => reader.Length < 8 ? "0" : reader.ReadUInt64().ToString(),
        ElementType.R4 => reader.ReadSingle().ToString(),
        ElementType.R8 => reader.ReadDouble().ToString(),
        ElementType.String => reader.ReadUtf16String((int)(reader.BytesLeft / 2)),
        _ => null,
    };
}
