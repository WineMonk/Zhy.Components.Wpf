# ZFormComboColumnAttribute 类


下拉列表框表单列



## Definition
**命名空间：** <a href="N_Zhy_Components_Wpf__Attribute__ZFormColumn.md">Zhy.Components.Wpf._Attribute._ZFormColumn</a>  
**程序集：** Zhy.Components.Wpf (在 Zhy.Components.Wpf.dll 中) 版本：1.0.0+3f7c1d1c11b95806e5b31f9f35d89bb6bf59c47e

**C#**
``` C#
public class ZFormComboColumnAttribute : ZFormComboItemAttribute
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  <a href="https://learn.microsoft.com/dotnet/api/system.attribute" target="_blank" rel="noopener noreferrer">Attribute</a>  →  <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute.md">ZFormItemAttribute</a>  →  <a href="T_Zhy_Components_Wpf__Attribute__ZFormItem_ZFormComboItemAttribute.md">ZFormComboItemAttribute</a>  →  ZFormComboColumnAttribute</td></tr>
</table>



## 示例


**C#**  
``` C#
[ZFormComboColumn("角 色", ItemsSourceProperty = nameof(Roles))]
public string Role { get => _role; set => SetProperty(ref _role, value); }
public List<string> Roles { get => _roles; set => _roles = value; }
```


## 构造函数
<table>
<tr>
<td><a href="M_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute__ctor.md">ZFormComboColumnAttribute</a></td>
<td>下拉列表框表单列</td></tr>
</table>

## 属性
<table>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute_Index.md">Index</a></td>
<td>索引，用于排序<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute_IsHideFormColumn.md">IsHideFormColumn</a></td>
<td>作为表单列时是否隐藏此项，默认false</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute_IsHideFormItem.md">IsHideFormItem</a></td>
<td>作为表单项时是否隐藏此项，默认false</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute_IsReadOnlyColumn.md">IsReadOnlyColumn</a></td>
<td>是否为只读列，默认false</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute_IsReadOnlyItem.md">IsReadOnlyItem</a></td>
<td>是否为只读项，默认false<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute_IsSearchProperty.md">IsSearchProperty</a></td>
<td>是否为查询列 查询时查询该列的值，则为true，否则为false。</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormItem_ZFormComboItemAttribute_ItemsSourceProperty.md">ItemsSourceProperty</a></td>
<td>数据项源属性名<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__ZFormItem_ZFormComboItemAttribute.md">ZFormComboItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute_MemberPath.md">MemberPath</a></td>
<td>成员路径 如为基础类型，则保持为空；如为引用类型，设置为相应成员属性名。<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute_Title.md">Title</a></td>
<td>标题<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormItemAttribute.md">ZFormItemAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute_Width.md">Width</a></td>
<td>列宽度</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormComboColumnAttribute_WidthUnit.md">WidthUnit</a></td>
<td>列宽度单位</td></tr>
</table>

## 参见


#### 引用
<a href="N_Zhy_Components_Wpf__Attribute__ZFormColumn.md">Zhy.Components.Wpf._Attribute._ZFormColumn 命名空间</a>  
