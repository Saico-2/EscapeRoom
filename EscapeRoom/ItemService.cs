using System.Collections.Generic;

namespace EscapeRoom2
{
    class ItemService
    {
        // TODO:
        // * implement multiple locations
        // *** 2-dimensional grid of locations
        // *** command "go" to move around rooms
        // *** property for items or locations to specify where each item is located

        // player's location
        static int playerRoom = 0;

        // descriptions of rooms when entering or looking around
        static string[] roomDesc = {
            "You're standing in the middle of a small, almost empty room."
        };

        public string enterRoom()
        {
            return roomDesc[playerRoom];
        }

        List<Item> items = new List<Item>()
        {
            new Item(
                "door",
                null,
                "There is an old door in front of you.",
                "The door looks very old, almost all of the brown paint has fallen off. The golden keyhole looks brand-new though.",

                new string[] {
                    "\nIt is currently locked.",
                    "\nIt has been unlocked with a key."
                },

                new string[] {
                    "The door is locked.",
                    "You open the door and see the outside world on the other side. You step out and are finally free. You win!"
                }
            ),

            new Item(
                "desk",
                null,
                "To your right is a desk.",
                "The desk is very dusty. There's a golden skeleton key lying on it.",

                new string[] {
                    null,
                    null
                },

                new string[] {
                    null,
                    null
                }
            ),

            new Item(
                "note",
                null,
                "There's also a note on the floor right in front of you.",
                "The note reads: \"To escape, you must know how to use keys.\"",

                new string[] {
                    null,
                    null
                },

                new string[] {
                    "The note reads: \"To escape, you must know how to use keys.\"",
                    "The note reads: \"To escape, you must know how to use keys.\""
                }
            ),

            new Item(
                "key",
                "door",
                null,
                "The skeleton key is golden and without a speck of dirt or rust on it, as if it was made just today.",

                new string[] {
                    null,
                    null
                },

                new string[] {
                    "You use the key to unlock the door.",
                    "The door is already unlocked."
                }
            ),

            new Item(
                "picture",
                null,
                "There's a picture above the desk.",
                "The picture shows a vast green field.",

                new string[] {
                    null,
                    null
                },

                new string[] {
                    null,
                    null
                }
            )
        };

        // find item's location in list
        int ItemNameToId(string item)
        {
            // find by comparing every item's name to name in command
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i].name)
                {
                    return i;
                }
            }
            return -1; // item not found
        }

        string LookAt(string item)
        {
            // description of room and items inside it
            if (item == "around")
            {
                // concatenate all descriptions, starting with current room
                string itemAll = $"{roomDesc[playerRoom]}\n";
                for (int i = 0; i < items.Count; i++)
                {
                    // only visible items, otherwise there'd just be an unnecessary newline
                    if(items[i].descAround != null)
                    {
                        itemAll += $"{items[i].descAround}\n"; // generic descriptions of items in room
                    }
                }
                itemAll = itemAll.Remove(itemAll.Length - 1); // remove unnecessary newline
                return itemAll;
            }
            // looking at a specific item
            else
            {
                // find by comparing every item's name to name in command
                for (int i = 0; i < items[i].name.Length; i++)
                {
                    if (item == items[i].name)
                    {
                        return $"{items[i].descLook} {items[i].descLookUsed[items[i].used]}"; // specific description and description of item's state
                    }
                }
                return $"Item \"{item}\" not found.";
            }
        }

        string Use(string item, string usedOn = "")
        {
            // get IDs of items so there's no need to do it multiple times for one item
            int itemA = ItemNameToId(item);
            int itemB = -2; // initially mark as invalid
            if (usedOn != "") { itemB = ItemNameToId(usedOn); } // if a second item is specified in command, find its ID

            // for cleaner code
            // initially mark as invalid item
            string useDesc = "";
            string itemUsedOn = "";

            // if valid item, assign proper values
            if (itemA >= 0)
            {
                useDesc = items[itemA].descUse[items[itemA].used]; // item useable if NOT null
                itemUsedOn = items[itemA].canUseOn;  // same as above
            }

            if ((itemA != -1) && (itemB != -1))
            {
                // if just using an item
                if (usedOn == "")
                {
                    // items that can by used with other items either way cannot be used by themselves
                    if ((useDesc != null) && (itemUsedOn == null))
                    {
                        string message = $"{useDesc}";
                        items[itemA].used = 1;
                        return message;
                    }
                    else
                    {
                        return $"The {item} cannot be used this way.";
                    }
                }

                // if using one item on another
                else
                {
                    // if the item can be successfully used on target item
                    //// compares IDs
                    if (ItemNameToId(itemUsedOn) == itemB)
                    {
                        string message = $"{useDesc}";
                        items[itemA].used = 1;
                        items[itemB].used = 1;
                        return message;
                    }
                    // if item combo is invalid
                    else
                    {
                        return $"Cannot use the {item} on a {usedOn}.";
                    }
                }
            }

            // at least one item in command does not exist
            else if (ItemNameToId(item) == -1)
            {
                return $"Item \"{item}\" not found.";
            }
            else
            {
                return $"Item \"{usedOn}\" not found.";
            }
        }

        // understand commands, decide which method to pass them to
        //// 1st word is command, 2nd word is target item
        //// if using an item on another, 3rd word must be "on", 4th word is target item
        public string ReadCommand(string command)
        {
            string[] com = command.Split(' ');

            switch (com[0])
            {
                case "l":
                case "look":
                case "lookat":
                    try
                    {
                        return LookAt(com[1]);
                    }
                    // if player doesn't specify what they're looking at
                    catch
                    {
                        return $"You look at nothing in particular.";
                    }

                case "u":
                case "use":
                    try
                    {
                        // if using one item on another
                        if (com.Length > 2)
                        {
                            if (com[2] == "on")
                            {
                                return Use(com[1], com[3]);
                            }
                            else
                            {
                                return "Invalid command.";
                            }
                        }
                        // if using an item by itself
                        else
                        {
                            return Use(com[1]);
                        }
                    }
                    // if player doesn't specify what they're using
                    catch
                    {
                        return "You use. That's it, you just use. I don't know how that works, don't ask me.";
                    }

                default:
                    return "Invalid command.";
            }
        }
    }
}
