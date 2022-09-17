using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


namespace Hoopsly.Internal
{
    public interface IInitilizable<T>
    {
        public Task<T> Initilize(string uuid);
    }

    public interface IInitilizable
    {
        public Task Initilize(string uuid);
    }
}
