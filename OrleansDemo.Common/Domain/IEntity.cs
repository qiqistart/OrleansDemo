namespace OrleansDemo.Common.Domain;

public interface IEntity
{

    //是否删除
    StatusEnum Status { get; set; }
    //在新增时生成值， 一般插入一条数据时，记录插入的时间
    DateTime Created { get; set; }
    //在新增或修改时生成值， 一般记录修改的时间。
    DateTime? Updated { get; set; }
}
public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
}