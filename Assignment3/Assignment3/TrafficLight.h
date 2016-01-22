#pragma once
#include "Structs.h"

class TrafficLight
{
public:
	TrafficLight(int x, int y);
	~TrafficLight();

	void draw(HDC drawcanvas);
	void update();
	void tick();
	lightState * getStatePtr();
	void setPos(float x, float y);

	void setStop();
	void setGo();
	
private:

	bool green = true;
	bool yellow = false;
	bool red = false;

	rectangle lightrect;
	lightState lights;

};

