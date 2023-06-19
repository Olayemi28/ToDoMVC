namespace UniqueTodoApplication.Entities
{
    public class TodoitemCategory : BaseEntity
    {
        public int TodoitemId{get;set;}

        public Todoitem Todoitem{get;set;}

        public int CategoryId{get;set;}

        public Category Category{get;set;}
    }
}