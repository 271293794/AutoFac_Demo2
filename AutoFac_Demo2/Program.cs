using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoFac_Demo2
{
    /// <summary>
    /// 虽然构造函数参数注入是将值传递给正在构造的组件的首选方法，
    /// 但您也可以使用属性或方法注入来提供值。
    /// </summary>
    class Program
    {
        static IContainer container ;
        class A
        {
            /// <summary>
            /// 作为类 A 的一个属性，不一定需要构造函数
            /// </summary>
            public B B { get; set; }

        }
        class B
        {
            public void M()
            {
                Console.WriteLine("这是类B中的M方法"); ;
            }
        }
        // 模似mvc 中的 【Global】 文件中的 【Application_Start】方法
        static void Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(typeof(B));
            builder.RegisterType(typeof(A));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();// 方法一
            #region 方法二
            var b = new B(); var a=new A();
            builder.RegisterInstance(b).AsSelf().PropertiesAutowired();
            builder.RegisterInstance(a).AsSelf().PropertiesAutowired();
            #endregion




            builder.Register(c => new A { B = c.Resolve<B>() }); //方法三
            // 此句一定要放在方法最后执行
            container = builder.Build();

        }

        static void Main(string[] args)
        {
            Start();

            var aa = container.Resolve<A>();
            aa.B.M();
            //new A().B.M(); // 不能达到效果，a.B 为空


        }
    }
}
