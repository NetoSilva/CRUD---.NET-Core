using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EventosIntegration.DataContracts
{
    [DataContract]
    public class EventoContract
    {
        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public DateTime DataInicio { get; set; }

        [DataMember]
        public DateTime DataFim { get; set; }
    }
}
