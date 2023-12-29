﻿namespace core.Specifications;

public class Pagination<T>
{
    public Pagination(int pageIndex, int pageSize,int count, IEnumerable<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Data = data;
        Count = count;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IEnumerable<T> Data { get; set; }
}