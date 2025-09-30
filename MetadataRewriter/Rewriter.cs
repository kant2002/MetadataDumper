using dnlib.DotNet;

namespace MetadataRewriter;

internal class Rewriter(ModuleDefMD module)
{
    public void RewriteTypes(Dictionary<uint, string> typeRewriteRules)
    {
        foreach (var rewriteRule in typeRewriteRules)
        {
            var rid = rewriteRule.Key ^ 0x02000000;
            var typeDef = module.ResolveTypeDef(rid);
            if (typeDef is null) continue;
            typeDef.Name = rewriteRule.Value;
        }
    }

    public void RewriteTypes(Dictionary<string, string> typeRewriteRules)
    {
        foreach (var rewriteRule in typeRewriteRules)
        {
            var typeDef = module.Find(rewriteRule.Key, true);
            if (typeDef is null) continue;
            typeDef.Name = rewriteRule.Value;
        }
    }

    public void RewriteMethods(Dictionary<uint, string> methodRewriteRules)
    {
        foreach (var rewriteRule in methodRewriteRules)
        {
            var rid = rewriteRule.Key ^ 0x06000000;
            var methodDef = module.ResolveMethod(rid);
            if (methodDef is null) continue;
            methodDef.Name = rewriteRule.Value;
        }
    }
}