# Project-Groceries
Simple C# application for creating a sorted grocery list

### TODO:
* Prompt user to add a new grocery if list of groceries is empty
* Implement filtering of the groceries list "properly" (Like it is done in the groceries list view)
* Disable OK-button in groceries list when no changes have been made
* Prompt user that changes to groceries will be saved/discarded
* Add hotkeys
  * [Delete] -> Delete/Remove selected grocery
  * [Ctrl + N] -> Create new grocery
  * [Ctrl + S] -> Save grocery list to file
* Add context menues to groceries list (new, edit, and delete)
* Confirmation message when deleting grocery
* Ensure total price tally is always synced with total price column
* Tweak tab behavior in all windows
* When resizing a column, have the others retain their size (if possible)
* Move constants to static class (Button image sizes etc)
* Restrict access modifiers as much as possible
* Enable loading of images via website URL (download it to local folder)

### Known issues:
* Editing groceries removes them from the grocery list
* Groceries are allowed to have negative prices
* Total price isn't centered
* There are some issues with the filter ("Mjölk" isn't recognized)
