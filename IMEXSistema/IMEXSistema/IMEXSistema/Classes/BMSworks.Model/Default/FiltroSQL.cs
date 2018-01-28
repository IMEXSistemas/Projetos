using System;
using System.Collections.Generic;

namespace BMSworks.Model
{
    /// <summary>
    /// Classe de transporte para modelar a entidade FiltroSQL.
    /// </summary>
    [Serializable]
    public class FiltroSQL
    {
        private Int32 idFiltro;
        private string descricao;
        private Int32 idUsuario;
        private string idTela;
        private string wheres;

        public FiltroSQL() { }

        public FiltroSQL(Int32 IdFiltro, string Descricao, Int32 IdUsuario, string IdTela, string Wheres)
        {
            this.idFiltro = IdFiltro;
            this.descricao = Descricao;
            this.idUsuario = IdUsuario;
            this.idTela = IdTela;
            this.wheres = Wheres;
        }

        public Int32 IdFiltro
        {
            get { return idFiltro; }
            set { idFiltro = value; }
        }
        public string Descricao
        {
            get { return descricao; }
            set { descricao = value; }
        }
        public Int32 IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }
        public string IdTela
        {
            get { return idTela; }
            set { idTela = value; }
        }
        public string Wheres
        {
            get { return wheres; }
            set { wheres = value; }
        }

    }
}
