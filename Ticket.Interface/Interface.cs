using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PWMIS.DataMap.Entity;

namespace Ticket.Interface
{
    public interface ILongID
    {
        long ID { get; set; }
    }

    public interface IIntID
    {
        long ID { get; set; }
    }

    public interface IStringID
    {
        string ID { get; set; }
    }


    public interface IIsDelete
    {
        bool IsDelete { get; set; }
    }

    public interface IUpdateBoolValue
    {
        bool UpdateBoolValue(string propertyName);
    }

    public interface IUpdateBoolValue2
    {
        bool UpdateBoolValue(string propertyName, EntityBase e);
    }

    public interface ICacheEvent
    {
        //void Subscribe(UpdateCacheEventSource evenSource);
        //void UnSubscribe(UpdateCacheEventSource evenSource);
        string[] GetCacheKeys();
        string GetCacheKeyRule();

    }
}
