# Project Review

## Applicant: Vikentiy Kolenko

---

### Issue Estimate and Execution Analysis

Note: the original Excel table of time estimates is available here, with updates on actual times: https://1drv.ms/x/s!AnfpSZLFfHQDpcJpYVY-6KZ-FmnXzQ?e=1TrU0p

Here is a list of the main tasks (mostly named by class name), their estimates, approximate actual time, and commentary on noteworthy things relating to them, such as reasons for slowdowns.

##### Coding Tasks:

- Generic Object Pool. Estimated: 10min; actual: about 10min. Pooling MonoBehaviours to avoid unnecessary Instantiate/Destroy operations. Note that the pool itself is not a MonoBehaviour, which reduces object clutter, but turned out to require some additional care when it comes to deinstantiation and scene changing.
- AbstractSpaceObject. Estimated: 20min; actual: about 20min.
-  Projectile. Estimated: 5min; actual: a mere couple of minutes at first, but then a few more to expand functionality, so about 5. A simple object for player and enemy bullets.
- Explosion (and ExplosionPool). Estimated: 10min; actual: about 12min. Was meant to be minimalistic, but some pool functionality had to be added.
- GenericFormation. Estimated: 15min; actual: about 20min. Base class that spawns many space objects along a grid. Used for protector and enemy formations. Turned out to be slightly heavier than initially intended.
- Protector. Estimated: 10min; actual: about 5min. Minimalistic, just hook up scale changes to SpaceObject's own events, and unhook them on disable.
- ProtectorFormation. Estimated: 5min; actual: about 8min. Had to return to this later due to an off-by-one error.
- FleetBehaviour (the higher-level object class itself). Estimated: 10min; actual: about a dozen. By itself it was mostly straightforward, aside from some fine-tuning to handle destruction properly. 
  - But the aggregate parts (EnemyShipBehaviour, FleetFormation, FleetCannons, FleetMover) took about twice longer than expected (total estimate was less than an hour, but actual execution took almost two). 
- PlayerShipBehaviour. The higher-level object wasn't estimated to take any noteworthy time, but took a few minutes. Input class, on the other hand, turned out to be trivial and I'm not sure why I took it more seriously at first. The ShipCannon (shooting behaviour) was estimated about right.
- CollisionDamageLogic was estimated at about 20 minutes, was done a bit faster than that, but I underestimated the possibility of eyes slipping over a wrong affiliation in the pairs list. Autotests helped sort that out but took a few more minutes.
- Battlefield. Originally estimated at 45 minutes as 'LevelBuilder'. Throughout work it became apparent that it would be inconvenient to have a class that *only* builds the level/battlefield. So the new Battlefield class expanded a bit, also sending events relating to the battle and dismantling the battlefield in the end.
- GameMain. Estimated at 1 hour. Actual time took closer to 50 minutes. I expected this class would be where I risk hitting a serious slowdown (instead of Fleet classes), but it actually turned simpler than I imagined, thanks to the functionality distributed among the lower-level things.
- Miscellaneous Tasks (Interfaces, generics, configuration &c.): hard to estimate, because these things tend to be done in tiny chunks intermixed with the more major classes. I estimated that maybe an hour will be spent on this. I think the actual time was slightly less, but this is probably one of the least accurate numbers in this review.
- Scoreboard. Estimated about 45 minutes, took maybe 40, and even that mostly due to needing to double-check everything related to the scoring formula. Autotesting pointed out that there was a discrepancy between the formula and the supplied array of numbers in the requirement document. I sent an email pointing out the discrepancy, continued work on the expectation that the formula takes priority, and left two tests side by side, with one favouring the formula and the other favouring the supplied array (thus the array-favouring test will fail until the discrepancy is resolved or the priority changed).
- Menu Behaviours (UIMenu). Estimated as four five-minute classes, possibly as strategies. In retrospect that kind of splitting seemed overkill, and so instead all menu behaviour is aggregated in one place. It's still slightly smaller/faster than I imagined.

Total coding time was estimated at about 7 hours, and actual was probably 7 and a half. Note that this was based on trying to measure time spent *actually coding*, i.e. not planning, not research, and not counting contemplation of programme structure when away from the screen. In fact, this project probably significantly benefitted from the fact that I had to take breaks doing other things, instead of just trying to sit down and write all scripts in one go. 

Another factor that influenced the time taken is the choice of structure. A small game like this one is typically possible to write with fewer classes and lines, but lumping more functionality together. Instead, I tried to go for more separation, customisation, and a potential for extension. In retrospect, perhaps some of the attempts to showcase these approaches were overzealous and as a result look heavy-handed.

Overall, the combination of the requirements and the limited time available (both the formal limit, and the actual scheduling of the interview) was challenging. It also involved getting a refresher - theoretical and/or practical - on some of the nuances of Unity, such as the relationship between interfaces and components. On the flip side, while the constraints were interesting *as an exercise*, coding like that *all* the time would probably be unsustainable.

##### Non-Coding Tasks:

- I estimated that it'll take an hour of tweaking the UI, but probably spent at most half as much on it. Since I spent more workhours on NGUI than on the newer Unity UI, I was worried about running into a complication somewhere.
- I thought I'll spend about 1½ hours (or maybe two, but I wrote down 1½) on prefabs and scene editing, but ended up with about one. 
- I predicted spending about an hour and a half on playtesting too, but spent at least two and a half. Most of it was spent closer to the end of the process, when the game was almost fully functional. Full (prefinal/final) level runs were probably the biggest contributed to time taken. The fact that I'm inclined to play on lower speeds is probably the main reason.

##### Unaccounted-For Time:

The time spent planning and estimating wasn't properly counted, but I think this part of the process was a major contributor to project completion. 