import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { Home } from "./Components/Home/Home";
import { IngridientsData } from "./Components/IngridientsData/IngridientsData";
import Header from "./Components/Header/Header";
import Exercises from "./Components/Exercises/Exercises";
import SectionTraining from "./Components/SectionTraining/SectionTraining";
import AchievementData from "./Components/AchievementData/AchievementData";
import Categories from "./Components/Categories/Categories";
import Login from "./Components/Login/Login"
import Trainings from "./Components/Trainings/Trainings";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { PremiumSubscription } from "./Components/PremiumSubscription/PremiumSubscription";
import DesignThemeData from "./Components/DesignTheme/DesignTheme";

// Entry point of application, adds App component to the index.html file
const root = ReactDOM.createRoot(document.getElementById("root"));
const user = localStorage.getItem("user");

root.render(
	<React.StrictMode>
		<BrowserRouter>
			{user ? <Header /> : null}
			<Routes>
          <Route path="/" element={user ? <Home /> : <Login />} />
          <Route path="/IngridientsData" element={user ? <IngridientsData /> : <Login />} />
          <Route path="/Exercises" element={user ? <Exercises /> : <Login />} />
          <Route path="/SectionTraining" element={user ? <SectionTraining /> : <Login />} />
          <Route path="/AchievementData" element={user ? <AchievementData /> : <Login />} />
          <Route path="/Categories" element={user ? <Categories /> : <Login />} />
          <Route path="/Login" element={user ? <Login /> : <Login />} />
          <Route path="/Trainings" element={user ? <Trainings /> : <Login />} />
          <Route path="/PremiumSubscriptions" element={user ? <PremiumSubscription /> : <Login />} />
          <Route path="/DesignThemes" element={user ? <DesignThemeData /> : <Login />} />
				{/* Default Router */}
				<Route path="/*" element={<Navigate to="/" />} />
			</Routes>
		</BrowserRouter>
	</React.StrictMode>
);
