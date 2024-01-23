# ZFormOperColumnButtonAttribute 类


表单操作列功能按钮



## Definition
**命名空间：** <a href="N_Zhy_Components_Wpf__Attribute__ZFormColumn.md">Zhy.Components.Wpf._Attribute._ZFormColumn</a>  
**程序集：** Zhy.Components.Wpf (在 Zhy.Components.Wpf.dll 中) 版本：1.0.0+3f7c1d1c11b95806e5b31f9f35d89bb6bf59c47e

**C#**
``` C#
public class ZFormOperColumnButtonAttribute : ZFormFuncButtonAttribute
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  <a href="https://learn.microsoft.com/dotnet/api/system.attribute" target="_blank" rel="noopener noreferrer">Attribute</a>  →  <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>  →  ZFormOperColumnButtonAttribute</td></tr>
</table>



## 示例


**C#**  
``` C#
[ZFormTextButtonColumn("档案路径", Index = 5, ButtonContent = "更 改", RelayCommandName = nameof(CommandModifyArchivesPath), Width = 200, WidthUnit = DataGridLengthUnitType.Pixel)]
public string ArchivesPath { get => _archivesPath; set => SetProperty(ref _archivesPath, value); }
```


## 构造函数
<table>
<tr>
<td><a href="M_Zhy_Components_Wpf__Attribute__ZFormColumn_ZFormOperColumnButtonAttribute__ctor.md">ZFormOperColumnButtonAttribute</a></td>
<td>表单操作列功能按钮</td></tr>
</table>

## 属性
<table>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute_ButtonContent.md">ButtonContent</a></td>
<td>按钮内容<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute_ButtonStyle.md">ButtonStyle</a></td>
<td>按钮样式<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute_Index.md">Index</a></td>
<td>索引，用于排序<br />(继承自 <a href="T_Zhy_Components_Wpf__Attribute__Base_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>。)</td></tr>
</table>

## 参见


#### 引用
<a href="N_Zhy_Components_Wpf__Attribute__ZFormColumn.md">Zhy.Components.Wpf._Attribute._ZFormColumn 命名空间</a>  
