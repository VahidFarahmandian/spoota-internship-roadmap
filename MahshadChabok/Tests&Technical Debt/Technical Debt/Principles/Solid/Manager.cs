using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Principles.Solid
{
    // Interface Segregation Principle (ISP)
    public interface IWorker
    {
        void Work();
    }

    public interface IHire
    {
        void Hire(int id);
    }

    public interface IFire
    {
        void Fire(int id);
    }
    public interface ITakingLeave
    {
        void TakingLeave(int id,int days);
    }

    public class Worker : IWorker, ITakingLeave
    {
        LinkedList<string> requests;
        public Worker(LinkedList<string> requests)
        {
            this.requests = requests;
        }

            void ITakingLeave.TakingLeave(int id, int days)
        {
            string req = "I with id= " + id + " want that leave for " + days + "days to rest";
            requests.AddLast(req);
        }

        void IWorker.Work()
        {
            
        }
    }

    public class Manager : IWorker, IHire, IFire
    {
        LinkedList<int> WorkersId =new LinkedList<int>();
        void IHire.Hire(int id )
        {
            WorkersId.AddLast(id);
        }

        void IFire.Fire(int id)
        {
            WorkersId.Remove(id);
        }

        void IWorker.Work()
        {
            
        }
    }
}
