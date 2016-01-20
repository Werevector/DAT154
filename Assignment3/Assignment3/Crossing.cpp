#include "stdafx.h"
#include "Crossing.h"


Crossing::Crossing()
{
	
	/*for (int i = Direction::NORTH; i < Direction::WEST; i++) {
		lights.push_back(new TrafficLight(100,100));
	}
	lights.push_back(new TrafficLight())*/
}


Crossing::~Crossing()
{
	vector<TrafficLight*>::iterator iter;
	for (iter = lights.begin(); iter != lights.end(); iter++) {
		delete (*iter);
	}
}

void Crossing::init(HINSTANCE hinst, rectangle win)
{
	window = win;

	crossRect.w = 300;
	crossRect.h = 300;

	crossRect.x = (window.w / 2) - (crossRect.w / 2);
	crossRect.y = (window.h / 2) - (crossRect.h / 2);

	//NORTH
	lights.push_back(new TrafficLight( crossRect.x+crossRect.w / 1.5, crossRect.y - 60 ));
	//EAST
	lights.push_back(new TrafficLight(crossRect.x+crossRect.w, crossRect.y+crossRect.h/2));
	//SOUTH
	lights.push_back(new TrafficLight(crossRect.x + crossRect.w / 4, crossRect.y + crossRect.h));
	//WEST
	lights.push_back(new TrafficLight(crossRect.x - 30, crossRect.y + crossRect.h / 8));

	lights[NORTH]->setStop();
	lights[SOUTH]->setStop();

	cars.push_back(new Car(400, 400, NORTH));
	cars[0]->loadBitMap(hinst, IDB_CAR ,600, 200);

}

void Crossing::draw(HDC drawcanvas)
{

	Rectangle(drawcanvas, crossRect.x, crossRect.y, crossRect.x + crossRect.w, crossRect.y + crossRect.h);
	//draw cars
	vector<Car*>::iterator carIter;
	for (carIter = cars.begin(); carIter != cars.end(); carIter++ ) {
		(*carIter)->draw(drawcanvas);
	}

	//draw traffic lights
	vector<TrafficLight*>::iterator iter;
	for (iter = lights.begin(); iter != lights.end(); iter++) {
		(*iter)->draw(drawcanvas);
	}
}

void Crossing::update()
{

}

void Crossing::tick()
{
	vector<TrafficLight*>::iterator iter;
	for (iter = lights.begin(); iter != lights.end(); iter++) {
		(*iter)->tick();
	}
}
