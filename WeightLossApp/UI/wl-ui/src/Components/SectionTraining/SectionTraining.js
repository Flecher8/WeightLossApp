import { useState, useEffect } from "react";
import React from "react";
import { constants } from "../../Constants";
import { Modal, Button, Form, Row, Col, Table, InputGroup, FormControl } from "react-bootstrap";
import ChangeSectionTraining from "../ChangeSectionTraining/ChangeSectionTraining";

function SectionTraining() {
	// SectionTraining data
	const [sectionTraining, setSectionTraining] = useState([]);
	const [section, setSection] = useState();

	const [addShow, setAddShow] = useState(false);
	const addHandleClose = () => setAddShow(false);
	const addHandleShow = () => setAddShow(true);

	const [editShow, setEditShow] = useState(false);
	const editHandleClose = () => setEditShow(false);
	const editHandleShow = () => setEditShow(true);

	const [search, setSearch] = useState("");
	const [filterParameters, setFilterParameters] = useState({
		Type: false
	});

	// Get SectionTraining data
	function getSectionTraining() {
		fetch(constants.API_URL + "SectionTraining")
			.then(res => res.json())
			.then(data => setSectionTraining(data));
	}

	// onLoad function
	useEffect(() => {
		getSectionTraining();
	}, []);

	// Show add modal function
	function addModalShow() {
		addHandleShow();
		setSection({
			Id: undefined,
			Type: ""
		});
	}

	return (
		<div className="Exercises container">
			{/* Create new item button */}
			<div className="container mt-5" align="right">
				<Button onClick={() => addModalShow()} variant="outline-primary">
					Create new exercise
				</Button>
			</div>
			{/* Create new item modal */}
			<Modal size="lg" centered show={addShow} onHide={addHandleClose}>
				<ChangeSectionTraining
					state={addHandleClose}
					section={section}
					setSection={setSection}
					getSectionTraining={getSectionTraining}
					method="POST"
					title="Add new section"
				/>
			</Modal>
		</div>
	);
}

export default SectionTraining;
