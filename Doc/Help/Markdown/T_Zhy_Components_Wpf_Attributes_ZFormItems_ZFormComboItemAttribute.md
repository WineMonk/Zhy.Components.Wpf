# ZFormComboItemAttribute 类


下拉列表框表单项



## Definition
**命名空间：** <a href="N_Zhy_Components_Wpf_Attributes_ZFormItems.md">Zhy.Components.Wpf.Attributes.ZFormItems</a>  
**程序集：** Zhy.Components.Wpf (在 Zhy.Components.Wpf.dll 中) 版本：1.0.0.3+0ef912daaad63c6be3b767fdaa872e9a6b5bfa88

**C#**
``` C#
public class ZFormComboItemAttribute : ZFormItemAttribute
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  <a href="https://learn.microsoft.com/dotnet/api/system.attribute" target="_blank" rel="noopener noreferrer">Attribute</a>  →  <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute.md">ZFormItemAttribute</a>  →  ZFormComboItemAttribute</td></tr>
<tr><td><strong>Derived</strong></td><td><a href="T_Zhy_Components_Wpf_Attributes_ZFormColumns_ZFormComboColumnAttribute.md">Zhy.Components.Wpf.Attributes.ZFormColumns.ZFormComboColumnAttribute</a></td></tr>
</table>



## 示例


**C#**  
``` C#
[ZFormComboItem("角 色", ItemsSourceProperty = nameof(Roles))]
public string Role { get => _role; set => SetProperty(ref _role, value); }
public List<string> Roles { get => _roles; set => _roles = value; }
```


## 构造函数
<table>
<tr>
<td><a href="M_Zhy_Components_Wpf_Attributes_ZFormItems_ZFormComboItemAttribute__ctor.md">ZFormComboItemAttribute</a></td>
<td>下拉列表框表单项</td></tr>
</table>

## 属性
<table>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute_Index.md">Index</a></td>
<td>索引，用于排序<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute_IsReadOnlyItem.md">IsReadOnlyItem</a></td>
<td>是否为只读项，默认false<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_ZFormItems_ZFormComboItemAttribute_ItemsSourceProperty.md">ItemsSourceProperty</a></td>
<td>数据项源属性名</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute_MemberPath.md">MemberPath</a></td>
<td>成员路径 如为基础类型，则保持为空；如为引用类型，设置为相应成员属性名。<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute_Title.md">Title</a></td>
<td>标题<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
</table>

## 参见


#### 引用
<a href="N_Zhy_Components_Wpf_Attributes_ZFormItems.md">Zhy.Components.Wpf.Attributes.ZFormItems 命名空间</a>  
