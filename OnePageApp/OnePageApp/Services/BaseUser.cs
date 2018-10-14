using Prism.Mvvm;

namespace OnePageApp.Services
{
    public class BaseUser : BindableBase
    {
        public string Login { set; get; }
        public string Name { set; get; }

        public override bool Equals(object obj)
        {
            if (!(obj is BaseUser))
                return false;

            var other = obj as BaseUser;

            if (Login != other.Login)
                return false;

            return true;
        }


        public static bool operator ==(BaseUser x, BaseUser y)
        {
            if (ReferenceEquals(x,null) && ReferenceEquals(y, null))
                return true;
            if (ReferenceEquals(x, null)) return false;
            return x.Equals(y);
        }

        public static bool operator !=(BaseUser x, BaseUser y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return this.Login.GetHashCode();
        }

    }
}