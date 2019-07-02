# Map-Routing
Algorithms course's project.

In recent days, transportation applications to move from one place to another has been evolving rapidly. The use of data from many users helps to make transportation better. Especially in reducing transportation time and avoid heavy traffic roads.
In this project, you are asked to find the least time to move from the source location to the destination location. And to find the path that achieves that time.
The map is represented as a graph. Intersections are represented by nodes and roads are represented by edges. Each road has a speed (in km/h). You may assume that all the vehicles moving at the road are moving with that speed.
The moving person can walk for at most R meters from the source location to get to the starting intersection. Then they ride a vehicle to move to another intersection close to the destination location. Then he can walk from the intersection to the destination location for at most R meters.
The walking speed is always 5 km/h (from the source location to the nearby intersection and from the node near to the destination to the destination location). 



•	The moving person walks with constant speed (5 km/h) in straight lines (from the source to the starting intersection and from the finishing intersection to the destination).
•	The moving person cannot walk more than R meters to move from the source location to the starting intersection. They also cannot walk more that R meters to move from the finishing intersection to the destination location.
•	The time to get in a vehicle or get out of a vehicle is negligible (is not taken into consideration).
•	The moving person will ride only one vehicle. They cannot get out of the vehicle unless they reached the final node.
•	The vehicle always moves with the road speed. It changes speed only if the road is changed.
•	The roads connect intersection in straight lines. They are also bidirectional roads.



Input
1.	Map description file, which consists of:
a.	Number of intersections.
b.	(x, y) coordinates of intersections.
c.	Number of roads.
d.	The indices of the 2 intersections that are connected by each road.
e.	The road length (in km).
f.	The speed of each road.
2.	Queries file, each query consists of:
a.	Source location.
b.	Destination location.
c.	Maximum walking Distance R.
Output
For each query, you should output:
1.	The shortest time to move from the source to the destination.
2.	The total distance of the path with the shortest time (vehicle moved distance and walked distance).
3.	The nodes of the path with the shortest time (starting from the starting node to the finishing node).
Test Cases
Small Cases
•	The number of intersections will not exceed 20 intersections.
•	The number of roads will not exceed 50 roads.
•	The number of queries will not exceed 10 queries.
Medium Cases
•	The number of intersections will not exceed 20000 intersections.
•	The number of roads will not exceed 25000 roads.
•	The number of queries will not exceed 1000 queries.
Large Cases
•	The number of intersections will not exceed 200000 intersections.
•	The number of roads will not exceed 250000 roads.
•	The number of queries will not exceed 1000 queries.
