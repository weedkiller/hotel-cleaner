namespace NawafizApp.Domain.Entities
{
    public class OrderEqp
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public bool needfix { set; get; }
        public bool ishere { set; get; }
        public virtual FixOrder FixOrder { set; get; }



    }
}