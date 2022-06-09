import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function AchivementData() {
	// AchivementData data
	const [achivementData, setAchivementData] = useState([]);
	const [achivement, setAchivement] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Name: false,
		Description: false,
		RewardExperience: false,
		ImgName: false
	});

	// Get achivement data
	function getAchivementData() {
		fetch(constants.API_URL + "AchivementData")
			.then(res => res.json())
			.then(data => setAchivementData(data));
	}

	// onLoad function
	useEffect(() => {
		getAchivementData();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setAchivementData({
			Id: undefined,
			Name: "",
			Description: "",
			RewardExperience: 0,
			ImgName: ""
		});
	}

	return (
		<div className="AchivementData">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new achivement
				</Button>
			</div>
		</div>
	);
}

export default AchivementData;
