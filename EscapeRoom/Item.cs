
namespace EscapeRoom
{
    class Item
    {
        // ------------------------------------------------------------------------
        // VARIABLES
        // ------------------------------------------------------------------------

        string name;           // used in commands
                                      //// (never set to null)

        string canUseOn;       // names of items that can be used with this item
                               //// (null if the item can only be used by itself)

        string descAround;     // general description of items when using "look around"
                               //// (null if the item should be hidden from the player)

        string descLook;       // specific description when using "look [name]"
                               //// (should never be null)

        string[] descLookUsed; // added at the end of descriptions depending on the state of the item
                               //// (always two elements: 0 before being used, 1 after being used)
                               //// (null if the item's state shouldn't change or be visible)

        string[] descUse;      // description of items when using "use [name]"
                               //// (always two elements: 0 for using first time, 1 for every subsequent use)
                               //// (null means the item cannot be used, always set both elements as null in this case)
        
        bool takeAble;         // specifies whether the item can the picked up
                               //// (true means item can be picked up, false means it doesn't fit in bag or is too heavy)
                               //// (true also means item can only be used from inventory)
                               //// (always include descUse if setting this to true)
        // TODO
        int location;          // which room the item belongs to
                               //// (-1 means player inventory, any value equal to or lower than -2 will make the item non-existent)

        int used;              // 0 means not used yet, 1 means successfully used
                               //// (should be 0 initially and changed to 1 after being successfully used)
                               //// (while type bool would make more sense, int is required for selecting proper description)


        // ------------------------------------------------------------------------
        // FIELDS
        // ------------------------------------------------------------------------

        public Item(
            string name = "thing",
            string canUseOn = null,
            string descAround = null,
            string descLook = "An unidentified object that shouldn't exist is floating nearby.",
            string[] descLookUsed = null,
            string[] descUse = null,
            bool takeAble = false,
            int location = -2,
            int used = 0
        ){
            this.name = name;
            this.canUseOn = canUseOn;
            this.descAround = descAround;
            this.descLook = descLook;
            this.descLookUsed = descLookUsed;
            this.descUse = descUse;
            this.takeAble = takeAble;
            this.location = location;
            this.used = used;
        }


        // ------------------------------------------------------------------------
        // PROPERTIES
        // ------------------------------------------------------------------------

        public string Name
        {
            get { return name; }
        }
        public string CanUseOn
        {
            get { return canUseOn; }
        }
        public string DescAround
        {
            get { return descAround; }
        }
        public string DescLook
        {
            get { return descLook; }
        }
        public string[] DescLookUsed
        {
            get { return descLookUsed; }
        }
        public string[] DescUse
        {
            get { return descUse; }
        }
        public bool TakeAble
        {
            get { return takeAble; }
        }
        public int Location
        {
            get { return location; }
            set { location = value; }
        }
        public int Used
        {
            get { return used; }
            set { used = value; }
        }
    }
}
