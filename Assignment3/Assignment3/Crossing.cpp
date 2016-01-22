#include "stdafx.h"
#include "Crossing.h"


Crossing::Crossing()
{
	
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
	lights.push_back(new TrafficLight(crossRect.x + 3 * (crossRect.w / 4), crossRect.y - 60 ));
	//EAST
	lights.push_back(new TrafficLight(crossRect.x+crossRect.w, crossRect.y+(crossRect.h/2) + 75));
	//SOUTH
	lights.push_back(new TrafficLight(crossRect.x + crossRect.w / 4 - 30, crossRect.y + crossRect.h));
	//WEST
	lights.push_back(new TrafficLight(crossRect.x - 30, crossRect.y + (crossRect.h / 8) - 22));

	lights[NORTH]->setStop();
	lights[SOUTH]->setStop();

	cars.push_back(new Car(300, 520, WEST, lights[WEST]));
	cars[0]->loadBitMap(hinst, IDB_CAR ,600, 200);
	//cars[0]->drive();

}

void Crossing::draw(HDC drawcanvas)
{
	HBRUSH pavementBrush = CreateSolidBrush(RGB(50,50,50));
	HBRUSH pav_middleBrush = CreateSolidBrush(RGB(255, 255, 0));
	HBRUSH intersectBrush = CreateHatchBrush(HS_CROSS, RGB(0, 0, 0));
	
	SelectObject(drawcanvas, pavementBrush);
	//WEST
	Rectangle(	drawcanvas,
				0,
				crossRect.y + (crossRect.h/4),
				window.w,
				crossRect.y + 3*(crossRect.h/4)
		);
	SelectObject(drawcanvas, pav_middleBrush);
	Rectangle(
				drawcanvas,
				0,
				crossRect.y + (crossRect.h / 2) - 5,
				window.w,
				crossRect.y + (crossRect.h /2) + 5
			 );
	
	//NORTH
	SelectObject(drawcanvas, pavementBrush);
	Rectangle(drawcanvas,
		crossRect.x + (crossRect.w / 4),
		0,
		crossRect.x + 3 * (crossRect.w / 4),
		window.h
		);
	SelectObject(drawcanvas, pav_middleBrush);
	Rectangle(
		drawcanvas,
		crossRect.x + (crossRect.w / 2) - 5,
		0,
		crossRect.x + (crossRect.w / 2) + 5,
		window.h
		);

	SelectObject(drawcanvas, intersectBrush);
	Rectangle(drawcanvas, crossRect.x, crossRect.y, crossRect.x + crossRect.w, crossRect.y + crossRect.h);

	DeleteObject(pavementBrush);
	DeleteObject(pav_middleBrush);
	DeleteObject(intersectBrush);

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
	vector<Car*>::iterator carIter;
	for (carIter = cars.begin(); carIter != cars.end(); carIter++) {
		(*carIter)->update();
	}
}

void Crossing::resize(rectangle newWind)
{
	window = newWind;
	crossRect.x = (window.w / 2) - (crossRect.w / 2);
	crossRect.y = (window.h / 2) - (crossRect.h / 2);

	lights[0]->setPos(crossRect.x + 3 * (crossRect.w / 4), crossRect.y - 60);
	lights[1]->setPos(crossRect.x + crossRect.w, crossRect.y + (crossRect.h / 2) + 75);
	lights[2]->setPos(crossRect.x + crossRect.w / 4 - 30, crossRect.y + crossRect.h);
	lights[3]->setPos(crossRect.x - 30, crossRect.y + (crossRect.h / 8) - 22);

}

void Crossing::tick()
{
	vector<TrafficLight*>::iterator iter;
	for (iter = lights.begin(); iter != lights.end(); iter++) {
		(*iter)->tick();
	}
}
