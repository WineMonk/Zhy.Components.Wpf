# ZFormDialog 构造函数


表单项对话框



## Definition
**命名空间：** <a href="N_Zhy_Components_Wpf__View__Window.md">Zhy.Components.Wpf._View._Window</a>  
**程序集：** Zhy.Components.Wpf (在 Zhy.Components.Wpf.dll 中) 版本：1.0.0

**C#**
``` C#
public ZFormDialog(
	ObservableObject observableObject,
	Func<ObservableObject, bool> funcValuesCheck = null
)
```



#### 参数
<dl><dt>  ObservableObject</dt><dd>表单项实例</dd><dt>  <a href="https://learn.microsoft.com/dotnet/api/system.func-2" target="_blank" rel="noopener noreferrer">Func</a>(ObservableObject, <a href="https://learn.microsoft.com/dotnet/api/system.boolean" target="_blank" rel="noopener noreferrer">Boolean</a>)  (Optional)</dt><dd>属性验证方法，输入为当前编辑的表单项实例，如验证各项属性符合要求则返回true，否则返回false</dd></dl>

## 参见


#### 引用
<a href="T_Zhy_Components_Wpf__View__Window_ZFormDialog.md">ZFormDialog 类</a>  
<a href="N_Zhy_Components_Wpf__View__Window.md">Zhy.Components.Wpf._View._Window 命名空间</a>  
