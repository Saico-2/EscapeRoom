
namespace EscapeRoom2
{
    class Item
    {
        public string name;
        public string canUseOn;
        public string descAround;
        public string descLook;
        public string[] descLookUsed;
        public string[] descUse;
        public int used;

        public Item(string name = null, string canUseOn = null, string descAround = null, string descLook = null, string[] descLookUsed = null, string[] descUse = null, int used = 0)
        {
            this.name = name;
            this.canUseOn = canUseOn;
            this.descAround = descAround;
            this.descLook = descLook;
            this.descLookUsed = descLookUsed;
            this.descUse = descUse;
            this.used = used;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string CanUseOn
        {
            get { return canUseOn; }
            set { canUseOn = value; }
        }
        public string DescAround
        {
            get { return descAround; }
            set { descAround = value; }
        }
        public string DescLook
        {
            get { return descLook; }
            set { descLook = value; }
        }
        public string[] DescLookUsed
        {
            get { return descLookUsed; }
            set { descLookUsed = value; }
        }
        public string[] DescUse
        {
            get { return descUse; }
            set { descUse = value; }
        }
        public int Used
        {
            get { return used; }
            set { used = value; }
        }
    }
}
