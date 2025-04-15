# ECS7016P - Interactive Agents and Procedural Generation - Assignment 3
By Harry McCoy - 230885554

## Design
### Stage 1 - Generation
The generation of the map is done via Binary Space Partitioning (BSP), a tree node structure is created at the first stage of generation, then the map is partitioned either vertically or horizontally, this is determined by the size of the current partitions, to ensure that no section becomes too small and will have to be discarded later. Expansions of the tree are done randomly in order to ensure more novelty in results, each expansion creates two child nodes each representing the two different sections, this continues until a certain amount of partitions is created. There are limitations put in place to ensure this partitioning runs effectively, the use of a tree depth limit ensures that partitions never get too small and promotes more diversity in expansion, a minimum number of islands is also put in place, the system will check to see if the final island count matches this value if not the random seed will be changed and a new generation will be created if no output reaches the desired amount by a certain amount of iterartions, the output will just and on the last generated map.

Once paritions are generated they are populated with random amounts of both floor and wall cells, code provided by Sebastian Lague "Procedural Cave Generation", the total amount of floor cells are totalled up and an additional check is made to make sure that islands meet a specific size requirement, if not all the cells within that partition are set to walls and are removed from the final list of islands, this final list is then checked against the minmum island amount mentioned above and regeneration is performed is necessary.

<img width="992" alt="Screenshot 2025-04-15 at 10 11 53" src="https://github.com/user-attachments/assets/5a5f69e1-84c9-4a55-8e3b-c6395b760b80" />

*Example output of Stage 1 without Stage 2 Cellular Automata Smoothing Applied*


<img width="997" alt="image" src="https://github.com/user-attachments/assets/5516fd46-c546-4046-93dd-d94d05a481b3" />

*Example output of same Stage 1 generation with Stage 2 Cellular Automata Smoothing Applied*

### Stage 3 - Processing
After smoothing is performed in Stage 2, code for which is provided by Sebastian Lague "Procedural Cave Generation", we process the map, the first thing that's applied is the removal of holes that aims to remove any large wall piece appearing within the middle of islands it does this by taking the island's bounds and going line by line in the x axis checking the distance between the last floor tile seen and the latest floor tile seen. If it is greater than than a max size, which is defined to make sure the algorithm isn't just smoothing out any edges to keep the islands outer structure imperfect much like an island, it will fill in the space it's found with floor tiles. After that smoothing is once again applied to clear up any artifacts that pop up, this process is applied twice in order to remove any holes the prvious smoothing may have created, although this number can be increased, however the greater the amount of loops the less the partitions retains their rough island-like exterior. Bridges are then then added between the islands to connect them, this is done by simply going from the center of each island and extruding outwards in each direction, if the pointer reaches another island a connection is created between the two islands by filling in the wall cells between them with floors, I put a hard limit that each island can only connect in a certain direction once, this was providing odd results with multiple overlapping bridges and overall bad output. From there the islands are populated with landmarks and npc's, the landmarks themselves spawn the NPC's. There are 4 types of landmark, city (for islands with a cell count of over 300), town (for islands with a cell count over 200 but less than 300), port (for islands with a cell count under 200) and a laboratory which will be randomly placed on an island along with it's regular landmark, this is responsible for spawning Zombie NPC's into the map.

<img width="985" alt="Screenshot 2025-04-15 at 10 16 14" src="https://github.com/user-attachments/assets/55cd29f8-66b8-4092-bd14-6b237540dd42" />

*Example output of Stage 1 without Stage 2 Cellular Automata Smoothing Applied*

### Additional Feature - Zombie NPC Simulation
  NPC's have two distinct types, Citizens and Zombies, the goals of which differ, the former wants to travel from island to island and survive, the latter wanting to infect any Citizen it sees'. To perform these actions there a number of different design principles the NPCs utilise, the movement system of the NPCs is tied to each of the landmarks, they only have vision scope of landmarks directly neighbouring the island they are currently inhabiting this works for both travelling and fleeing to different islands, in addition Citizen's keep a track of islands they have visited that have become, "unsafe" meaning they have encountered a Zombie there. The NPCs contain two collider one that acts as a vision radius, when other NPCs enter that collider that is set to trigger they become aware of each other, which will have differing effects depending on situations and the second smaller one registers collisions between the gameObjects with other gameObjects, solely used for Zombie infecting.

<img width="507" alt="image" src="https://github.com/user-attachments/assets/fd2112e1-6666-4f1e-b15a-e39b1a8ea40c" />

*Example NPC*

#### NPC Job System ###
- Roam

  The roaming behaviour is simple, within the bounds of the current island the NPC inhabits it will pick random points and the navmesh will move the gameObject towards them, when they reach that point, another point will be generated.


#### Zombie Exclusive Job ###
- Seek

  The Seeking system works off of an adversary design where when a Citizen enters their outmoster collider, they will set their adversarial NPC to that citizen, their navmesh will the navigate them towards the players position, until they are able to either infect them, by colliding their innermost collider with that of the Citizen's or a closer Citizen gets into their range at which time they will change subject and pursue them instead. If a Zombie enters their outermost collider whilst one is seeking however it will also gain the same adversary NPC, or in the case of both having an adversary the closest one will be chosen, this provides a swarm like intelligence between the Zombies.
- Infection

  When the Zombies innermost collider touches that of a Citizen, the Citizen's type will change to also become a Zombie at which point both will return to their normally roaming behaviour.
#### Citizen Exclusive Job ###
- Travel to Island

  An NPC has a 1/1000 chance every frame to decide to leave their current island to move to one of it's neighbouring islands it will then sets it's NavMesh navigation point to the location of the landmark in their destination island. Once it has reached it's destination this will becomes it's new home and will starting roaming around this new island. 

- Flee
  
  Fleeing works on a similar system to the Zombies Seeking job, when a Zombie enters their outtermost colldier they will assign them to their adversary and set their current island to "unsafe" by removing it from a list and will set their destination to another island like Travel to Island the NPC will check it's neighbouring islands, it will then calculate the distance between them and those landmarks, it will head to whichever is btoh included in the safe islands list and has the shortest distance. Citizen's who enter the outtermost collider of other citizens they will share the adversary and fleeing status and will flee to the same new island, simulating communication between the Citizens.

## Design Brief Completion

My design brief was Zombie Apocalypse: The player is a survivor of a zombie apocalypse trying to locate an abandoned research lab. I believe that I have successfully created outputs that fuffill the requirements set by my brief, my system creates a number of rough looking paritions that are analagous to islands, making this a specific design philosphy I carried throughout when coding the project. I also believe my additional feature goes a long way in also fulfilling this brief, not only adding to the map an abandoned research lab but also providing a simulation of how Zombies and Citizens would react during the scenario presented, this providing a good look into how the mechanics of this game may look.

## Example Outputs

<img width="983" alt="image" src="https://github.com/user-attachments/assets/e00e84a0-9e9a-4ffc-abb9-23bd14ea859f" />

<img width="987" alt="image" src="https://github.com/user-attachments/assets/0a6045a5-677e-4b8a-ad2b-e300a2e5bd2b" />

<img width="991" alt="image" src="https://github.com/user-attachments/assets/da70fc22-600c-4d62-bd76-e793b5042c46" />

### Acknowledgments
Code was adapted from Based on "Procedural Cave Generation" by Sebastian Lague https://www.youtube.com/watch?v=v7yyZZjF1z4&list=PLFt_AvWsXl0eZgMK_DT5_biRkWXftAOf9 for inital provided materials, the smoothing script (can be found in MapGenerator.cs lines 262 - 308) and adapted the randomly placing of cell types (can be found in MapGenerator.cs 176- 206)

External Library Unity AI Navigation - The NavMesh and NavMesh Agents had to be imported in order to control NPC movement - https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/index.html 
