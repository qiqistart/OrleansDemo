using Orleans;

namespace OrleansGrains.DemoGrains.DemoIGrains
{
    public interface IHalloGrains : IGrainWithStringKey
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task Hallo();
    }
}
