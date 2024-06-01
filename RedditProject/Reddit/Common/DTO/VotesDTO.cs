using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.PeerResolvers;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class VotesDTO
    {
        public VotesDTO(int up, int down)
        {
            Up = up;
            Down = down;
        }

        public int Up{get; set;}
        public int Down{get; set;}



    }
}
