using System.Reflection;
using Scriban;
namespace SpaceBattle.Lib;

public class AdapterBuilder : IBuilder
{
    private Type _type1;
    private Type _type2;
    private string _template_text = @"public class {{type2}}Adapter : {{type2}}
    {
        private {{type1}} obj;

        public {{type2}}Adapter({{type1}} obj) => this.obj = obj;

        {{- for propInfo in (props)}}

        public {{propInfo.property_type.name}} {{propInfo.name}}
        {
            {{if propInfo.get_method != null}}get => IoC.Resolve<{{propInfo.property_type.name}}>(""Get{{propInfo.name}}"", obj);{{ end }}
            {{if propInfo.set_method != null}}set => IoC.Resolve<ICommand>(""Set{{propInfo.name}}"", obj, value).Execute();{{ end }}
        }{{ end }}
    }";
    private Template _template;
    
    private IList<PropertyInfo> _props = new List<PropertyInfo>();

    public AdapterBuilder(Type type1, Type type2)
    {
        _type1 = type1;
        _type2 = type2;
        _template = Template.Parse(_template_text);
    }

    public void AddMembers(object property)
    {
        _props.Add((PropertyInfo)property);
    }

    public string Build()
    {
        return _template.Render(new { type2 = _type2.Name, type1 = _type1.Name, props = _props});
    }
}
