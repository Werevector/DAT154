#include "stdafx.h"
#include "Crossing.h"

BitMap loadBitMap(HINSTANCE hInst, int IDB, int w, int h);

Crossing::Crossing()
{
	pw = 0;
	pn = 0; 
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
	this->hinst = hinst;
	crossRect.w = 300;
	crossRect.h = 300;

	crossRect.x = (window.w / 2) - (crossRect.w / 2);
	crossRect.y = (window.h / 2) - (crossRect.h / 2);

	//NORTH
	lights.push_back(new TrafficLight(crossRect.x + 3 * (crossRect.w / 4), crossRect.y - 60 ));
	//WEST
	lights.push_back(new TrafficLight(crossRect.x - 30, crossRect.y + (crossRect.h / 8) - 22));

	lights[NORTH]->setStop();

	backGround = loadBitMap(hinst, IDB_BACKGROUND, 600, 600);
	
}

void Crossing::placeCar(Direction dir) {
	Car* ncar;

	bool empty;

	switch (dir) {
	
	case WEST:
		empty = westcars.size() == 0;
		if (!empty)
			if (westcars[westcars.size() - 1]->getBounding()->x < 50)
				break;

		ncar = new Car(-200, 490, dir, lights[dir]->getStatePtr());
		ncar->loadBitMap(hinst, IDB_CAR, 600, 200);
		
		if(!empty)
		ncar->setNextCar(westcars[westcars.size() - 1]->getBounding());
		
		westcars.push_back(ncar);
		break;
	case NORTH:
		empty = northcars.size() == 0;

		if (!empty)
			if (northcars[northcars.size() - 1]->getBounding()->y < 50)
				break;

		ncar = new Car(500, -200, dir, lights[dir]->getStatePtr());
		ncar->loadBitMap(hinst, IDB_BITMAP2, 200, 600);
		
		if (!empty)
		ncar->setNextCar(northcars[northcars.size() - 1]->getBounding());
		
		northcars.push_back(ncar);
	}
}

void Crossing::draw(HDC drawcanvas)
{
	HBRUSH pavementBrush = CreateSolidBrush(RGB(50,50,50));
	HBRUSH pav_middleBrush = CreateSolidBrush(RGB(255, 255, 0));
	//HBRUSH intersectBrush = CreateHatchBrush(HS_CROSS, RGB(0, 0, 0));
	HBRUSH intersectBrush = CreateSolidBrush(RGB(50, 50, 50));
	
	HDC bcDC = CreateCompatibleDC(drawcanvas);

	SelectObject(bcDC, backGround.image);

	SetStretchBltMode(bcDC, BLACKONWHITE);
	StretchBlt(drawcanvas, 0, 0, window.w, window.h, bcDC, 0, 0, backGround.width, backGround.height, SRCCOPY);

	DeleteObject(bcDC);

	wstring sp = _T("Cars north: " + to_wstring(northcars.size()));
	wstring a = _T("Cars west: " + to_wstring(westcars.size()));

	wstring pnS = _T("pn: " + to_wstring(pn));
	wstring pwS = _T("pw: " + to_wstring(pw));

	TextOut(drawcanvas, 10, 10, sp.c_str(), sp.size());
	TextOut(drawcanvas, 10, 26, a.c_str(), a.size());

	TextOut(drawcanvas, 10, 60, pnS.c_str(), pnS.size());
	TextOut(drawcanvas, 10, 76, pwS.c_str(), pwS.size());

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
	//West
	vector<Car*>::iterator carIter;
	for (carIter = westcars.begin(); carIter != westcars.end(); carIter++ ) {
		(*carIter)->draw(drawcanvas);
	}
	//North
	for (carIter = northcars.begin(); carIter != northcars.end(); carIter++) {
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
	/*if (GetAsyncKeyState(VK_DOWN)) { pn -= 10; };
	if (GetAsyncKeyState(VK_UP)) { pn += 10; };
	if (GetAsyncKeyState(VK_LEFT)) { pw -= 10; };
	if (GetAsyncKeyState(VK_RIGHT)) { pw += 10; };*/

	vector<Car*>::iterator carIter;
	for (carIter = westcars.begin(); carIter != westcars.end(); carIter++) {
		(*carIter)->update(crossRect);
	}
	for (carIter = northcars.begin(); carIter != northcars.end(); carIter++) {
		(*carIter)->update(crossRect);
	}
	checkOutofBounds();

}

void Crossing::checkOutofBounds() {
	for (int i = 0; i < northcars.size(); i++) {
		if (northcars[i]->outsideRect(window)) {
			if(northcars.size() != 1)
				northcars[i + 1]->freeNextCar();

			delete northcars[i];
			northcars.erase(northcars.begin() + i);
		}
	}
	for (int i = 0; i < westcars.size(); i++) {
		if (westcars[i]->outsideRect(window)) {
			if (westcars.size() != 1)
				westcars[i + 1]->freeNextCar();

			delete westcars[i];
			westcars.erase(westcars.begin() + i);
		}
	}
}

void Crossing::resize(rectangle newWind)
{
	window = newWind;
	crossRect.x = (window.w / 2) - (crossRect.w / 2);
	crossRect.y = (window.h / 2) - (crossRect.h / 2);

	lights[0]->setPos(crossRect.x + 3 * (crossRect.w / 4), crossRect.y - 60);
	lights[1]->setPos(crossRect.x - 30, crossRect.y + (crossRect.h / 8) - 22);

}

void Crossing::tick()
{
	int spawnWest = (int)(rand() % 100) + 1;
	int spawnNorth = (int)(rand() % 100) + 1;

	if(spawnWest <= pw)
	placeCar(WEST);
	
	if(spawnNorth <= pn)
	placeCar(NORTH);

	vector<TrafficLight*>::iterator iter;
	for (iter = lights.begin(); iter != lights.end(); iter++) {
		(*iter)->tick();
	}
}

void Crossing::pwUP()
{
	pw += 10;
}

void Crossing::pwDOWN()
{
	pw -= 10;
}

void Crossing::pnUP()
{
	pn += 10;
}

void Crossing::pnDOWN()
{
	pn -= 10;
}

BitMap loadBitMap(HINSTANCE hInst, int IDB, int w, int h)
{
	BitMap bitMap;
	bitMap.image = LoadBitmap(hInst, MAKEINTRESOURCE(IDB));
	bitMap.width = w;
	bitMap.height = h;
	return bitMap;
}