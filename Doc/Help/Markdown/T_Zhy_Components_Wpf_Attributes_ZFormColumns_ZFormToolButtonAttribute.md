# ZFormToolButtonAttribute 类


表单操作列功能按钮



## Definition
**命名空间：** <a href="N_Zhy_Components_Wpf_Attributes_ZFormColumns.md">Zhy.Components.Wpf.Attributes.ZFormColumns</a>  
**程序集：** Zhy.Components.Wpf (在 Zhy.Components.Wpf.dll 中) 版本：1.0.0.3+0ef912daaad63c6be3b767fdaa872e9a6b5bfa88

**C#**
``` C#
public class ZFormToolButtonAttribute : ZFormFuncButtonAttribute
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  <a href="https://learn.microsoft.com/dotnet/api/system.attribute" target="_blank" rel="noopener noreferrer">Attribute</a>  →  <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>  →  ZFormToolButtonAttribute</td></tr>
</table>



## 示例


**C#**  
``` C#
[ZFormToolButton("全 选", Index = 0, Dock = Dock.Right, ButtonStyle = ZFormButtonStyle.DefaultButton, Location = ButtonLocation.Bottom)]
public RelayCommand<IList?> CommandCheckTotalItem => new RelayCommand<IList?>(CheckTotalItem);
```


## 构造函数
<table>
<tr>
<td><a href="M_Zhy_Components_Wpf_Attributes_ZFormColumns_ZFormToolButtonAttribute__ctor.md">ZFormToolButtonAttribute</a></td>
<td>表单操作列功能按钮</td></tr>
</table>

## 属性
<table>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute_ButtonContent.md">ButtonContent</a></td>
<td>按钮内容<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute_ButtonStyle.md">ButtonStyle</a></td>
<td>按钮样式<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_ZFormColumns_ZFormToolButtonAttribute_Dock.md">Dock</a></td>
<td>停靠位置，默认左侧</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute_Index.md">Index</a></td>
<td>索引，用于排序<br />(继承自 <a href="T_Zhy_Components_Wpf_Attributes_Bases_ZFormFuncButtonAttribute.md">ZFormFuncButtonAttribute</a>。)</td></tr>
<tr>
<td><a href="P_Zhy_Components_Wpf_Attributes_ZFormColumns_ZFormToolButtonAttribute_Location.md">Location</a></td>
<td>按钮位置，默认表单上方</td></tr>
</table>

## 参见


#### 引用
<a href="N_Zhy_Components_Wpf_Attributes_ZFormColumns.md">Zhy.Components.Wpf.Attributes.ZFormColumns 命名空间</a>  
