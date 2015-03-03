# DNN-DeleteUnauthorisedUsers
Delete Unauthorised DNN Users

The sole porpose of this project, at this time, is to soft delete un-authorised users on a schduled task.

On installation a .dll is placed in the bin folder and a schedule item is added to the db Schedule table. The initial values for the schedule are that it will fire once per hour with a re-try period of 30 minutes. A single stored procedure is created to do the soft deleting of the selected users.

As this is not a Desktop module I chose to place its home folder in  "\admin\Delete Unauthorised Users". I am not sure if this is acceptable but this can easily be changed.

<h2>March 3rd 2015</h2>
This module now offers an option to remove deleted users.
