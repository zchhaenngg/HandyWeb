1.EntityFramework在删除数据时，什么情况下会删除关联的中间表数据？如何删除关联的中间表数据?（导航属性）
   解决方案：如果中间表就是导航属性所在对象，remove之，如中间表由>=3个表的Id组成。调用 source.remove(当前对象.导航属性)。
	         如果导航属性所在对象和当前数据是一对多关系，即中间表由2个表的Id组成。调用 当前对象.导航属性.Clear()方法。
2.EntityFramework删除导航属性所在的对象？
    解决方案：通过导航属性找到要删除的对象  remove之。
EntityFramework
1.支持负责类型如枚举。
2.支持将实体映射到多个表。
  https://msdn.microsoft.com/zh-cn/data/jj715646
