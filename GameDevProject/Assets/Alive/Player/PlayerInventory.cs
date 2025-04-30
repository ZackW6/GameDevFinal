using System.Collections.Generic;

public class PlayerInventory : Inventory
{
    public List<Item> dropped;
    public List<PlayerSlot> slots;
    public void addDropped(Item dropped){
        if (!this.dropped.Contains(dropped)){
            this.dropped.Add(dropped);
        }
        
        slots.ForEach((data)=>{
            if (data.droppedItem && data.droppedItem == dropped){
                data.droppedItem = null;
            }
        });

        equippedItems.Remove(dropped);
        unequippedItems.Remove(dropped);
    }

    public bool removeDropped(Item dropped){
        return this.dropped.Remove(dropped);
    }

    public override void checkEquipped(){
        slots.ForEach((data)=>{
            if (data.droppedItem && data.droppedItem is Item){
                if (data.item == PlayerSlot.extra){
                    addUnequipped((Item)data.droppedItem);
                    dropped.Remove((Item)data.droppedItem);
                }else{
                    addEquipped((Item)data.droppedItem);
                    dropped.Remove((Item)data.droppedItem);
                }
            }
        });
        base.checkEquipped();
    }
}