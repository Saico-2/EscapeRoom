using System.Collections.Generic;
using System;

namespace EscapeRoom
{
    class ItemService
    {
        // TODO:
        // 
        // * implement multiple locations
        // *** grid of locations, either in a 2- or 3-dimensional list/array or only use IDs and let grid be an illusion
        // *** property for items or locations to specify where each item is located
        // *** command "go" to move through doors
        // ***** doors as special item type?
        // ***** property specifying which location it goes to or -1 if it's not a door?


        // ------------------------------------------------------------------------
        // TEMPORARY
        // ------------------------------------------------------------------------

        // player's location
        int playerLocation = 0;

        // descriptions of rooms when entering or looking around
        string[] roomDesc = {
            "You're standing in the middle of a small, almost empty room."
        };


        // ------------------------------------------------------------------------
        // IN-GAME ITEMS
        // ------------------------------------------------------------------------

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
                },
                false,
                0
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
                },
                false,
                0
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
                },
                false,
                0
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
                },
                true,
                0
            )
        };


        // ------------------------------------------------------------------------
        // METHODS
        // ------------------------------------------------------------------------

        public string enterRoom()
        {
            return roomDesc[playerLocation];
        }

        // find item's location in list
        int ItemNameToId(string item)
        {
            // find by comparing every item's name to name in command
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i].Name)
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
                string itemAll = $"{roomDesc[playerLocation]}\n";
                for (int i = 0; i < items.Count; i++)
                {
                    // only visible items and items from the same location as player
                    if ((items[i].DescAround != null) && (items[i].Location == playerLocation))
                    {
                        itemAll += $"{items[i].DescAround}\n"; // generic descriptions of items in room
                    }
                }
                itemAll = itemAll.Remove(itemAll.Length - 1); // remove unnecessary newline at the end
                return itemAll;
            }
            // looking at a specific item
            else
            {
                // find by comparing every item's name to name in command
                for (int i = 0; i < items.Count; i++)
                {
                    if (item == items[i].Name)
                    {
                        return $"{items[i].DescLook} {items[i].DescLookUsed[items[i].Used]}"; // specific description and description of item's state
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
                useDesc = items[itemA].DescUse[items[itemA].Used]; // item useable if NOT null
                itemUsedOn = items[itemA].CanUseOn;  // same as above
            }

            // using happens here
            if ((itemA != -1) && (itemB != -1))
            {
                // if just using an item
                if (usedOn == "")
                {
                    // if the item can be in inventory
                    if (items[itemA].TakeAble)
                    {
                        // if the item is currently in inventory
                        if (items[itemA].Location == -1)
                        {
                            // items that can be used with other items either way cannot be used by themselves
                            if (itemUsedOn == null)
                            {
                                string message = $"{useDesc}";
                                items[itemA].Used = 1;
                                return message;
                            }
                            else
                            {
                                return $"The {item} cannot be used this way.";
                            }
                        }
                        // items that can be in inventory cannot be used outside of inventory
                        else
                        {
                            return $"The {item} cannot be used this way.";
                        }
                    }
                    // if the item cannot be taken
                    else
                    {
                        // items that can be used with other items either way cannot be used by themselves
                        if ((useDesc != null) && (itemUsedOn == null))
                        {
                            string message = $"{useDesc}";
                            items[itemA].Used = 1;
                            return message;
                        }
                        else
                        {
                            return $"The {item} cannot be used this way.";
                        }
                    }
                }
                // if using one item on another
                else
                {
                    // if the item can be in inventory
                    if (items[itemA].TakeAble)
                    {
                        // if the item is currently in inventory
                        if (items[itemA].Location == -1)
                        {
                            // if the item can be successfully used on target item
                            //// compares IDs
                            if (ItemNameToId(itemUsedOn) == itemB)
                            {
                                string message = $"{useDesc}";
                                items[itemA].Used = 1;
                                items[itemB].Used = 1;
                                return message;
                            }
                            // if item combo is invalid
                            else
                            {
                                return $"Cannot use the {item} on a {usedOn}.";
                            }
                        }
                        // items that can be in inventory cannot be used outside of inventory
                        else
                        {
                            return $"The {item} cannot be used this way.";
                        }
                    }
                    // if the item cannot be taken
                    else
                    {
                        // if the item can be successfully used on target item
                        //// compares IDs
                        if (ItemNameToId(itemUsedOn) == itemB)
                        {
                            string message = $"{useDesc}";
                            items[itemA].Used = 1;
                            items[itemB].Used = 1;
                            return message;
                        }
                        // if item combo is invalid
                        else
                        {
                            return $"Cannot use the {item} on a {usedOn}.";
                        }
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

        string Take(string item)
        {
            // find by comparing every item's name to name in command
            for (int i = 0; i < items.Count; i++)
            {
                if (item == items[i].Name)
                {
                    // true if the item can be picked up
                    if (items[i].TakeAble)
                    {
                        if (items[i].Location != -1)
                        {
                            // change item's location to inventory
                            items[i].Location = -1;
                            return $"You put the {item} in your bag.";
                        }
                        else
                        {
                            return "It's already in your bag.";
                        }
                    }
                    else
                    {
                        return "Cannot take this item.";
                    }
                }
            }
            return $"Item \"{item}\" not found.";
        }

        string Inventory()
        {
            // concatenate all item names
            string itemAll = $"";
            for (int i = 0; i < items.Count; i++)
            {
                // only items in inventory
                if (items[i].Location == -1)
                {
                    itemAll += $"{items[i].Name}\n";
                }
            }
            itemAll = itemAll.Remove(itemAll.Length - 1); // remove unnecessary newline at the end
            return itemAll;
        }

        string Go(string destination)
        {
            return "TODO";
        }

        // understand commands, decide which method to pass them to
        //// 1st word is command, 2nd word is target item
        //// if using an item on another, 3rd word must be "on", 4th word is target item
        public string ReadCommand(string command)
        {
            // store each word of command as different element of array
            string[] com = command.Split(' ');

            // call proper method depending on player's command
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
                        return "You stare at an empty wall.";
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
                        return "You use. That's it, you just use. Don't ask me, you're the one who entered that command.";
                    }

                case "t":
                case "p":
                case "take":
                case "pick":
                case "pickup":
                    try
                    {
                        return Take(com[1]);
                    }
                    // if player doesn't specify what they're using
                    catch
                    {
                        return "You put the nothing in your bag.";
                    }

                case "i":
                case "inv":
                case "inventory":
                    try
                    {
                        return Inventory();
                    }
                    catch (Exception error)
                    {
                        return $"Something went horribly wrong.\n{error}";
                    }

                case "g":
                case "go":
                    try
                    {
                        return Go(com[1]);
                    }
                    catch
                    {
                        return "You pace around thinking about what to do next.";
                    }

                default:
                    return "Invalid command.";
            }
        }
    }
}
