#pragma once
#include <vector>
#include <string>
#include "TrafficLight.h"
#include "Structs.h"

using namespace std;
class Car
{
public:
	Car(int x, int y, Direction dir, lightState *lightptr);
	~Car();

	void draw(HDC drawcanvas);
	void update(rectangle crossing);
	void drive();
	void halt();

	bool loadBitMap(HINSTANCE hInst, int IDB, int w, int h);


private:
	
	bool driving = true;
	Point position;
	Vector2d velocity;
	float acceleration;
	BitMap bitMap;
	Direction direction;

	void updateMovement();
	void updateState(rectangle crossing);

	Vector2d maxSpeed;
	float maxAcceleration;
	float breakSpeed = 2;

	rectangle boundingBox;

	lightState *lightptr = nullptr;

};

