
namespace EscapeRoom2
{
    class Item
    {
        public string name;           // used in commands
                                      //// (never set to null)

        public string canUseOn;       // names of items that can be used with this item
                                      //// (null if the item can only be used by itself)

        public string descAround;     // general description of items when using "look around"
                                      //// (null if the item should be hidden from the player)

        public string descLook;       // specific description when using "look [name]"
                                      //// (should never be null)

        public string[] descLookUsed; // added at the end of descriptions depending on the state of the item
                                      //// (always two elements: 0 before being used, 1 after being used)
                                      //// (null if the item's state shouldn't change or be visible)

        public string[] descUse;      // description of items when using "use [name]"
                                      //// (always two elements: 0 for using first time, 1 for every subsequent use)
                                      //// (null means the item cannot be used, always set both elements as null in this case)

        public int used;              // should be 0 initially and changed to 1 after being successfully used
                                      //// (defaults to 0, unnecessary to include when creating items)

        public Item(
            string name = "thing",
            string canUseOn = null,
            string descAround = null,
            string descLook = "An unidentified object that shouldn't exist is floating nearby.",
            string[] descLookUsed = null,
            string[] descUse = null,
            int used = 0
        ){
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
