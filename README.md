mw2-server
=================================================================================

This tool provides the ability to create a simulated COD:MW2 server.  

=================================================================================

To configure the server, click on the little help box on the topright of the tool.

- Hostname: Hostname of your server
- Gametype: Gametype of your server (note that the gametype may not contain any spaces!)
- Mapname: Mapname (fill in anything you want, even if the map doesn't exist)
- Maxclients: Max. amount of clients on this server
- Clients: Current amount of clients on the server
- Fs_game: Server mod (e.g. 'mods/Testmod' - This will also be displayed as gametype, if your gametype is valid)
- Port: UDP Port the server runs on (Changing it requires to restart the tool)
- Error: Error message that gets displayed to clients upon joining
- Spam: Enter any text you want, this will get displayed on every clients console when updating the serverlist

=================================================================================

Note:  
There is a way to use certain exploits to cause other clients to crash.  
Placing a '^' followed by either a 0x01 or a 0x02 byte (0x01 : '' - 0x02: '') in either the hostname,  
mapname, gametype or fs_game will cause the clients to crash when updating the serverlist.  
Placing it in the error message will cause them to crash upon joiing.  
I fixed this in the hostname on RepZ client as part of a deal, but as they didn't stick to it,  
I won't either, so feel free to exploit them ;)
