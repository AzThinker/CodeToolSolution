ATK 设计框架辅助工具-代码生成器

在 ATK框架代码中的示例，是用代码生成器生成的。
示例中有三个项目DemoTools.BLL 业务层，DemoTools.UIServer 前端服务层，DemoTools.WebUI 前端是ASP.CORE项目，项目本身的生成是通过VS来生成，业务代码是由工具生成的，为使数据能显示出来，只在下两处修改了代码

1、AzCustOrderHistController下的

	public IActionResult Index()
        {
            var paramItem = EngineContext.Current.Resolve<AzCustOrderHistListWebDto>();
            // 手动加入条件参数；
            paramItem.P_CustomerID = "ALFKI";
            var model = _handle.GetList(paramItem);
            return View(model);
        }

2、_Layout.cshtml文件中的
   <div class="navbar-collapse collapse">
	<ul class="nav navbar-nav">
	    <li><a asp-area="" asp-controller="AzOrders" asp-action="Index">订单</a></li>
	    <li><a asp-area="" asp-controller="AzProducts" asp-action="Index">产品</a></li>
	    <li><a asp-area="" asp-controller="AzCustOrderHist" asp-action="Index">订单历史查询</a></li>
	</ul>
    </div>
3、生成的案例部分显示

 ![image](https://github.com/AzThinker/CodeToolSolution/tree/master/WinCodeView/Remark/201809190933294.png）

其他工作：
1、数据库，数据库使用的是标准Northwind数据库，不过原生的Northwind数据库不支持中文，需要更改其字符集。

2、使用工具对Northwind数据库进行分析；
https://github.com/AzThinker/CodeToolSolution/raw/master/WinCodeView/Remark/201809190933292.png
https://github.com/AzThinker/CodeToolSolution/tree/master/WinCodeView/Remark/201809190933291.jpg
3、生在代码。

https://github.com/AzThinker/CodeToolSolution/tree/master/WinCodeView/Remark/201809190933293.png

----
ATK
1、一个完整支持分布式服务框架；
2、代码生成工具，可快速生成基于服务框架的应用；
3、其他支持库
4、完整代码可在GitHub的https://github.com/azthinker ；开源中国 https://gitee.com/azthinker
目标：使应用开发，低代码、高效率、快迭代
有兴趣的请在QQ群中参与讨论、联系作者  QQ群名称：ATK高效开发 ,QQ群号：747049962
也可发邮件至：azthinker@sina.com
Demo中的数据库、编译好的代码工具链接：https://pan.baidu.com/s/1B9RQm7_-SFyhLb_HLIGs0w 密码：85q6
ATK Demo使用的数据库 ： https://download.csdn.net/download/xftyyyyb/10675497
ATK自动代码工具 ：https://download.csdn.net/download/xftyyyyb/10675490

