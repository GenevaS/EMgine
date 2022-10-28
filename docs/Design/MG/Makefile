TARGET=EMgine_MG
LATEX=pdflatex -synctex=1 -interaction=nonstopmode -shell-escape

all:	
	$(LATEX) $(TARGET).tex && \
	bibtex $(TARGET).aux && \
	$(LATEX) $(TARGET).tex && \
	$(LATEX) $(TARGET).tex

clean:
	rm -f *.pdf *.out *.dvi *aux *bbl *blg *log *toc *.ptb *.tod *.fls *.fdb_latexmk *.lof *.xdv *.xcp