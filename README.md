# ECS7016P - Interactive Agents and Procedural Generation - Assignment 3
By Harry McCoy - 230885554

## Design
### Stage 1 - Generation
The generation of the map is done via Binary Space Partitioning (BSP), a tree node structure is created at the first stage of generation, then the map is partitioned either vertically or horizontally, this is determined by the size of the current partitions, to ensure that no section becomes too small and will have to be discarded later. Expansions of the tree are done randomly in order to ensure more novelty in results, each expansion creates two child nodes each representing the two different sections, this continues until a certain amount of partitions is created. There are limitations put in place to ensure this partitioning runs effectively, the use of a tree depth limit ensures that partitions never get too small and promotes more diversity in expansion, a minimum number of islands is also put in place, the system will check to see if the final island count matches this value if not the random seed will be changed and a new generation will be created if no output reaches the desired amount by a certain amount of iterartions, the output will just and on the last generated map.

Once paritions are generated they are populated with random amounts of both floor and wall cells, code provided by Sebastian Lague "Procedural Cave Generation", the total amount of floor cells are totalled up and an additional 

### Stage 3 - Processing

### Additional Feature - Zombie NPC Simulation
  NPC's have two distinct types, Citizens and Zombies, the goals of which differ, the former wants to travel from island to island and survive, the latter wanting to infect any Citizen it sees'. To perform these actions there a number of different design principles the NPCs utilise, the movement system of the NPCs is tied to each of the landmarks, they only have vision scope of landmarks directly neighbouring the island they are currently inhabiting this works for both travelling and fleeing to different islands, in addition Citizen's keep a track of islands they have visited that have become, "unsafe" meaning they have encountered a Zombie there. The NPCs contain two collider one that acts as a vision radius, when other NPCs enter that collider that is set to trigger they become aware of each other, which will have differing effects depending on situations and the second smaller one registers collisions between the gameObjects with other gameObjects, solely used for Zombie infecting.

<img width="507" alt="image" src="https://github.com/user-attachments/assets/fd2112e1-6666-4f1e-b15a-e39b1a8ea40c" />

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
### Acknowledgments
