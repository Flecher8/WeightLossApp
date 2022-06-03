import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import Header from "./Components/Header/Header";
import Home from "./Components/Home/Home";
import Ingridients from "./Components/Ingridients/Ingridients";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
	<BrowserRouter>
		<Header />
		<Routes>
			<Route path="/" element={<Home />} />
			<Route path="/home" element={<Home />} />
			<Route path="/IngridientData" element={<Ingridients />} />
			{/* Default Router */}
			<Route path="/*" element={<Navigate to="/" />} />
		</Routes>
	</BrowserRouter>
);

