import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import { Home } from "./Components/Home/Home";
import { IngridientsData } from "./Components/IngridientsData/IngridientsData";
import Header from "./Components/Header/Header";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

// Entry point of application, adds App component to the index.html file
const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
	<React.StrictMode>
		<BrowserRouter>
			<Header />
			<Routes>
				<Route path="/" element={<Home />} />
				<Route path="/IngridientsData" element={<IngridientsData />} />

				{/* Default Router */}
				<Route path="/*" element={<Navigate to="/" />} />
			</Routes>
		</BrowserRouter>
	</React.StrictMode>
);
