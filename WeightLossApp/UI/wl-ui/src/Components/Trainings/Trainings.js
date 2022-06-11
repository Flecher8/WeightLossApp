import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";

function Trainings() {
	// Trainings data
	const [trainings, setTrainings] = useState([]);
	const [training, setTraining] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	// Get trainings data
	function getTrainings() {
		fetch(constants.API_URL + "Trainigs")
			.then(res => res.json())
			.then(data => setTrainings(data));
	}

	// onLoad function
	useEffect(() => {
		getTrainings();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setTraining({
			Id: undefined,
			Section: 0,
			Complexity: ""
		});
	}

	return (
		<div className="Trainings">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new training
				</Button>
			</div>
		</div>
	);
}

export default Trainings;
