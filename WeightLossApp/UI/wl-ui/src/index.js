import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { Home } from "./Components/Home/Home";
import { IngridientsData } from "./Components/IngridientsData/IngridientsData";
import Header from "./Components/Header/Header";
import Exercises from "./Components/Exercises/Exercises";
import SectionTraining from "./Components/SectionTraining/SectionTraining";
import AchievementData from "./Components/AchievementData/AchievementData";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { PremiumSubscription } from "./Components/PremiumSubscription/PremiumSubscription";

// Entry point of application, adds App component to the index.html file
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
	<React.StrictMode>
		<BrowserRouter>
			<Header />
			<Routes>
				<Route path="/" element={<Home />} />
				<Route path="/IngridientsData" element={<IngridientsData />} />
				<Route path="/Exercises" element={<Exercises />} />
				<Route path="/SectionTraining" element={<SectionTraining />} />
				<Route path="/AchievementData" element={<AchievementData />} />
				<Route path="/PremiumSubscription" element={<PremiumSubscription />} />
				{/* Default Router */}
				<Route path="/*" element={<Navigate to="/" />} />
			</Routes>
		</BrowserRouter>
	</React.StrictMode>
);
