enum AndOr {
    And = 1,
    Or = 0
}
class HyQueryItem {
    ///运算符“与”或“或”
    public andor: AndOr;
}
class HyQuery {
    //查询名称
    public name: string;
    //public hyQueryItems: List<HyQueryItem>
}